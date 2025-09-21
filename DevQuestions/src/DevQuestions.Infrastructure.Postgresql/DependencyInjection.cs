using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.Postgresql.Questions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Database;

namespace DevQuestions.Infrastructure.Postgresql;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IQuestionsRepository, QuestionsSqlRepository>();

        services.AddDbContext<QuestionsReadDbContext>();

        return services;
    }
}