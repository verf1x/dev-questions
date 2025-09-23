using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using Shared;

namespace DevQuestions.Infrastructure.Communication;

public class UsersCommunicationService : IUsersCommunicationService
{
    public Task<Result<long, ErrorsList>>
        GetUserRatingAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<Result<long, ErrorsList>>(0L);
    }
}