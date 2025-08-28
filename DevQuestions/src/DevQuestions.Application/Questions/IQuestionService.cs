using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionService
{
    Task<Guid> CreateAsync(CreateQuestionRequest request, CancellationToken cancellationToken = default);
}