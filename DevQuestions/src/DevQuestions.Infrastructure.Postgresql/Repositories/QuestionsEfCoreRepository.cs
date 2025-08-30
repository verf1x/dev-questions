using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgresql.Repositories;

public class QuestionsEfCoreRepository : IQuestionsRepository
{
    private readonly QuestionsDbContext _dbContext;

    public QuestionsEfCoreRepository(QuestionsDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken = default)
    {
        await _dbContext.Questions.AddAsync(question, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public async Task<Question?> GetByIdAsync(Guid questionId, CancellationToken cancellationToken = default)
    {
        var question = await _dbContext.Questions
            .Include(q => q.Answers)
            .Include(q => q.Solution)
            .FirstOrDefaultAsync(q => q.Id == questionId, cancellationToken);

        return question;
    }

    public Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}