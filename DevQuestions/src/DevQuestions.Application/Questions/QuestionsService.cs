using CSharpFunctionalExtensions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Failures;
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

    public async Task<Result<Guid, ErrorsList>> CreateAsync(
        CreateQuestionRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var calculator = new QuestionCalculator();

        var calculationResult = calculator.Calculate();
        if (calculationResult.IsFailure)
            return calculationResult.Error;

        int openedUserQuestionCount = await _questionsRepository
            .GetOpenedUserQuestionsCountAsync(request.UserId, cancellationToken);

        if (openedUserQuestionCount > 3)
            return Errors.Questions.TooManyQuestions().ToErrors();

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

public class QuestionCalculator
{
    public Result<int, ErrorsList> Calculate()
    {
        return Error.Failure("", "").ToErrors();
    }
}