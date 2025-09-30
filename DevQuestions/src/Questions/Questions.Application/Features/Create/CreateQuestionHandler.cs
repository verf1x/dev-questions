using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Questions.Application.Failures;
using Questions.Domain;
using Shared;
using Shared.Abstractions;

namespace Questions.Application.Features.Create;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand, Guid>
{
    private readonly ILogger<CreateQuestionHandler> _logger;
    private readonly IQuestionsRepository _questionsRepository;

    public CreateQuestionHandler(
        ILogger<CreateQuestionHandler> logger,
        IQuestionsRepository questionsRepository)
    {
        _logger = logger;
        _questionsRepository = questionsRepository;
    }

    public async Task<Result<Guid, ErrorsList>> HandleAsync(
        CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        int openedUserQuestionCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(request.CreateQuestionDto.UserId, cancellationToken);

        if (openedUserQuestionCount > 3)
            return Errors.Questions.TooManyQuestions().ToErrors();

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            request.CreateQuestionDto.Title,
            request.CreateQuestionDto.Text,
            request.CreateQuestionDto.UserId,
            null,
            request.CreateQuestionDto.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question with ID {QuestionId} created.", questionId);

        return questionId;
    }
}