using CSharpFunctionalExtensions;

namespace Shared.Abstractions;

public interface ICommand;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand
{
    Task<Result<TResponse, ErrorsList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<UnitResult<ErrorsList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}