using System.Data;

namespace Shared.Database;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}