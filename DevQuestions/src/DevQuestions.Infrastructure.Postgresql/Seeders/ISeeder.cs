namespace DevQuestions.Infrastructure.Postgresql.Seeders;

public interface ISeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}