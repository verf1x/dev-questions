using CSharpFunctionalExtensions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken = default);

    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken = default);

    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken = default);

    Task<Result<Question, ErrorsList>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken = default);

    Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken = default);
}