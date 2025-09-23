using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.AddAnswer;

public class AddAnswerHandler : ICommandHandler<AddAnswerCommand, Guid>
{
    private readonly IValidator<AddAnswerDto> _validator;
    private readonly IUsersCommunicationService _communicationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<AddAnswerHandler> _logger;

    public AddAnswerHandler(
        IValidator<AddAnswerDto> validator,
        IUsersCommunicationService communicationService,
        IUnitOfWork unitOfWork,
        IQuestionsRepository questionsRepository,
        ILogger<AddAnswerHandler> logger)
    {
        _validator = validator;
        _communicationService = communicationService;
        _unitOfWork = unitOfWork;
        _questionsRepository = questionsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorsList>> HandleAsync(AddAnswerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var userRatingResult =
            await _communicationService.GetUserRatingAsync(command.AddAnswerDto.UserId, cancellationToken);
        if (userRatingResult.IsFailure)
            return userRatingResult.Error;

        if (userRatingResult.Value <= 0)
            return Errors.Questions.NotEnoughRating();

        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;
        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation(
            "Answer with ID {answerId} added to question {questionId}",
            answer.Id,
            command.QuestionId);

        return answer.Id;
    }
}