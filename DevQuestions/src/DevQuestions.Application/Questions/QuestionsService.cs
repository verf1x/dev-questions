using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Failures.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionRequest> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        IValidator<CreateQuestionRequest> validator,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Guid> CreateAsync(
        CreateQuestionRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        int openedUserQuestionCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(request.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        if (openedUserQuestionCount > 3)
        {
            throw new TooManyQuestionsException();
        }

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            request.Title,
            request.Text,
            request.UserId,
            null,
            request.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question with ID {QuestionId} created.", questionId);

        return questionId;
    }

    // public async Task<IActionResult> UpdateAsync(
    //     Guid questionId,
    //     UpdateQuestionRequest request,
    //     CancellationToken cancellationToken = default)
    // {
    // }
    //
    // public async Task<IActionResult> DeleteAsync(
    //     Guid questionId,
    //     CancellationToken cancellationToken = default)
    // {
    // }
    //
    // public async Task<IActionResult> SelectSolution(
    //     Guid questionId,
    //     Guid answerId,
    //     CancellationToken cancellationToken = default)
    // {
    // }
    //
    // public async Task<IActionResult> AddAnswersAsync(
    //     Guid questionId,
    //     AddAnswerRequest request,
    //     CancellationToken cancellationToken = default)
    // {
    // }
}