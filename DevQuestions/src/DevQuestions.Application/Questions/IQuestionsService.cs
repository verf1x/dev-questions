using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Guid> CreateAsync(CreateQuestionRequest request, CancellationToken cancellationToken = default);
}