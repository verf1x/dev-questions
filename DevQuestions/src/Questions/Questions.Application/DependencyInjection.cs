﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Questions.Application.Decorators;
using Shared.Abstractions;

namespace Questions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<,>),
                typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.TryDecorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator<,>));
        services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator<,>));

        return services;
    }
}