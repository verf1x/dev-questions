using CSharpFunctionalExtensions;
using Shared;

namespace DevQuestions.Application.Communication;

public interface IUsersCommunicationService
{
    Task<Result<long, ErrorsList>> GetUserRatingAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}