using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Questions.Application;
using Questions.Application.Failures;
using Questions.Application.Features.GetQuestionsWithFilters;
using Questions.Domain;
using Shared;

namespace Questions.Infrastructure.Postgres;

public class QuestionsEfCoreRepository : IQuestionsRepository
{
    private readonly QuestionsReadDbContext _readDbContext;

    public QuestionsEfCoreRepository(QuestionsReadDbContext context)
    {
        _readDbContext = context;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken = default)
    {
        await _readDbContext.Questions.AddAsync(question, cancellationToken);

        await _readDbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken = default)
    {
        _readDbContext.Questions.Attach(question);
        await _readDbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public async Task<Result<Question, ErrorsList>> GetByIdAsync(
        Guid questionId,
        CancellationToken cancellationToken = default)
    {
        var question = await _readDbContext.Questions
            .Include(q => q.Answers)
            .Include(q => q.Solution)
            .FirstOrDefaultAsync(q => q.Id == questionId, cancellationToken);

        if (question is null)
            return Errors.General.NotFound(questionId).ToErrors();

        return question;
    }

    public Task<(IReadOnlyList<Question> questions, long count)> GetWithFiltersAsync(
        GetQuestionsWithFiltersQuery query,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}