namespace DevQuestions.Infrastructure.Postgresql.Seeders;

public class QuestionsSeeder : ISeeder
{
    private readonly QuestionsDbContext _dbContext;

    public QuestionsSeeder(QuestionsDbContext context)
    {
        _dbContext = context;
    }

    public Task SeedAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
}