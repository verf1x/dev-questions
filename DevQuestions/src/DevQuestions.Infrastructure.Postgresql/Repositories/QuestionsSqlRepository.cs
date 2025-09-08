using CSharpFunctionalExtensions;
using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Infrastructure.Postgresql.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public QuestionsSqlRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken = default)
    {
        const string sql = """
                           INSERT INTO questions (id, title, text, user_id, attachment_id, tags, status)
                           VALUES (@Id, @Title, @Text, @UserId, @AttachmentId, @Tags, @Status)
                           """;

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            Id = question.Id,
            Title = question.Title,
            Text = question.Text,
            UserId = question.UserId,
            AttachmentId = question.AttachmentId,
            Tags = question.Tags.ToArray(),
            Status = question.Status,
        });

        return question.Id;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task<Result<Question, ErrorsList>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public async Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}