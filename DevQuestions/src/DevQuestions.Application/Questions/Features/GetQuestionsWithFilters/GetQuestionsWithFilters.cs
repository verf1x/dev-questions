using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IQueryHandler<GetQuestionsWithFiltersQuery, QuestionResponse>
{
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsReadDbContext _tagsReadDbContext;
    private readonly IQuestionsReadDbContext _questionsReadDbContext;

    public GetQuestionsWithFilters(
        IFilesProvider filesProvider,
        ITagsReadDbContext tagsReadDbContext,
        IQuestionsReadDbContext questionsReadDbContext)
    {
        _filesProvider = filesProvider;
        _tagsReadDbContext = tagsReadDbContext;
        _questionsReadDbContext = questionsReadDbContext;
    }

    public async Task<QuestionResponse> HandleAsync(
        GetQuestionsWithFiltersQuery query,
        CancellationToken cancellationToken)
    {
        var questions = await _questionsReadDbContext.ReadQuestions
            .Include(q => q.Solution)
            .Skip(query.Dto.Page * query.Dto.Limit)
            .Take(query.Dto.Limit)
            .ToListAsync(cancellationToken);

        long count = await _questionsReadDbContext.ReadQuestions.LongCountAsync(cancellationToken);

        var screenshotIds = questions
            .Where(q => q.AttachmentId is not null)
            .Select(q => q.AttachmentId!.Value);

        var filesDict = await _filesProvider.GetUrlsByIdsAsync(screenshotIds, cancellationToken);

        var questionTags = questions.SelectMany(q => q.Tags);

        var tags = await _tagsReadDbContext.ReadTags
            .Where(t => questionTags.Contains(t.Id))
            .Select(t => t.Name)
            .ToListAsync(cancellationToken);

        var questionsDto = questions.Select(q =>
            new QuestionDto(
                q.Id,
                q.Title,
                q.Text,
                q.UserId,
                (q.AttachmentId is not null ? filesDict[q.AttachmentId.Value] : null)!,
                q.Solution?.Id,
                tags,
                q.Status.ToRussianString()));

        return new QuestionResponse(questionsDto, count);
    }
}