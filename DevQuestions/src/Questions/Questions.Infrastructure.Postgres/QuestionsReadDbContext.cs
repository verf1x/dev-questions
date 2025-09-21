namespace Questions.Infrastructure.Postgres;

public class QuestionsReadDbContext : DbContext, IQuestionsReadDbContext
{
    public DbSet<Question> Questions { get; set; }

    public IQueryable<Question> ReadQuestions => Questions.AsNoTracking().AsQueryable();
}