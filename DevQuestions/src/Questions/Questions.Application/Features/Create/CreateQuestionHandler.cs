using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Failures;
using Questions.Domain;
using Shared;
using Shared.Abstractions;
using Shared.Extensions;

namespace Questions.Application.Features.Create;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand, Guid>
{
    private readonly ILogger<CreateQuestionHandler> _logger;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;

    public CreateQuestionHandler(
        ILogger<CreateQuestionHandler> logger,
        IQuestionsRepository questionsRepository,
        IValidator<CreateQuestionDto> validator)
    {
        _logger = logger;
        _questionsRepository = questionsRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorsList>> HandleAsync(
        CreateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.CreateQuestionDto, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        int openedUserQuestionCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(command.CreateQuestionDto.UserId, cancellationToken);

        if (openedUserQuestionCount > 3)
            return Errors.Questions.TooManyQuestions().ToErrors();

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            command.CreateQuestionDto.Title,
            command.CreateQuestionDto.Text,
            command.CreateQuestionDto.UserId,
            null,
            command.CreateQuestionDto.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question with ID {QuestionId} created.", questionId);

        return questionId;
    }
}