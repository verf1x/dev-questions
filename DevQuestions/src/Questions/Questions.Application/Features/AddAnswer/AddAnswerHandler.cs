using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Questions.Domain;
using Shared;
using Shared.Abstractions;

namespace Questions.Application.Features.AddAnswer;

public class AddAnswerHandler : ICommandHandler<AddAnswerCommand, Guid>
{
    // private readonly IUnitOfWork _unitOfWork;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<AddAnswerHandler> _logger;

    public AddAnswerHandler(
        // IUnitOfWork unitOfWork,
        IQuestionsRepository questionsRepository,
        ILogger<AddAnswerHandler> logger)
    {
        // _unitOfWork = unitOfWork;
        _questionsRepository = questionsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorsList>> HandleAsync(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        // var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(request.QuestionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;
        var answer = new Answer(Guid.NewGuid(), request.AddAnswerDto.UserId, request.AddAnswerDto.Text, request.QuestionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        // transaction.Commit();

        _logger.LogInformation(
            "Answer with ID {answerId} added to question {questionId}",
            answer.Id,
            request.QuestionId);

        return answer.Id;
    }
}