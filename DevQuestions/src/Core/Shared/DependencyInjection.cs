using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;

namespace Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}