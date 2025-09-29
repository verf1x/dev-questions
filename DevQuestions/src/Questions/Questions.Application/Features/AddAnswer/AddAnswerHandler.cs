﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Contracts.Dtos;
using Questions.Domain;
using Shared;
using Shared.Abstractions;
using Shared.Database;
using Shared.Extensions;

namespace Questions.Application.Features.AddAnswer;

public class AddAnswerHandler : ICommandHandler<AddAnswerCommand, Guid>
{
    private readonly IValidator<AddAnswerDto> _validator;
    // private readonly IUnitOfWork _unitOfWork;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<AddAnswerHandler> _logger;

    public AddAnswerHandler(
        IValidator<AddAnswerDto> validator,
        // IUnitOfWork unitOfWork,
        IQuestionsRepository questionsRepository,
        ILogger<AddAnswerHandler> logger)
    {
        _validator = validator;
        // _unitOfWork = unitOfWork;
        _questionsRepository = questionsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorsList>> HandleAsync(
        AddAnswerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        // var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;
        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        // transaction.Commit();

        _logger.LogInformation(
            "Answer with ID {answerId} added to question {questionId}",
            answer.Id,
            command.QuestionId);

        return answer.Id;
    }
}