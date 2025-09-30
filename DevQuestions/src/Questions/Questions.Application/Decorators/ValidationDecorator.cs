using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Abstractions;
using Shared.Extensions;

namespace Questions.Application.Decorators;

public class LoggingDecorator<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : ILogging
{
    private readonly ICommandHandler<TCommand, TResponse> _inner;
    private readonly ILogger<LoggingDecorator<TCommand, TResponse>> _logger;

    public LoggingDecorator(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<LoggingDecorator<TCommand, TResponse>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<Result<TResponse, ErrorsList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling command {CommandName}", typeof(TCommand).Name);

        var result = await _inner.HandleAsync(command, cancellationToken);

        _logger.LogInformation("Result: {result}", result);

        return result;
    }
}

public class ValidationDecorator<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : IValidation
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;
    private readonly ICommandHandler<TCommand, TResponse> _inner;

    public ValidationDecorator(
        IEnumerable<IValidator<TCommand>> validators,
        ICommandHandler<TCommand, TResponse> inner)
    {
        _validators = validators;
        _inner = inner;
    }

    public async Task<Result<TResponse, ErrorsList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await _inner.HandleAsync(command, cancellationToken);

        var context = new ValidationContext<TCommand>(command);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(f => !f.IsValid)
            .ToList();

        if (failures.Count > 0)
            return failures.ToErrors();

        return await _inner.HandleAsync(command, cancellationToken);
    }
}