namespace Questions.Infrastructure.Postgres;

public class QuestionsSeeder : ISeeder
{
    private readonly QuestionsReadDbContext _readDbContext;

    public QuestionsSeeder(QuestionsReadDbContext context)
    {
        _readDbContext = context;
    }

    public Task SeedAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
}