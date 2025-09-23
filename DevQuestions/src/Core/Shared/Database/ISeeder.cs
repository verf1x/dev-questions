namespace Shared.Database;

public interface ISeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}