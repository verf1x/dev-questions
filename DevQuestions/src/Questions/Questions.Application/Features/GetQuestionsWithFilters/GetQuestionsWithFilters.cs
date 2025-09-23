using Microsoft.EntityFrameworkCore;
using Questions.Contracts.Dtos;
using Questions.Contracts.Responses;
using Questions.Domain;
using Shared.Abstractions;
using Shared.FilesStorage;
using Tags.Contracts;
using Tags.Contracts.Dtos;

namespace Questions.Application.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IQueryHandler<GetQuestionsWithFiltersQuery, QuestionResponse>
{
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsContract _tagsContract;
    private readonly IQuestionsReadDbContext _questionsReadDbContext;

    public GetQuestionsWithFilters(
        IFilesProvider filesProvider,
        ITagsContract tagsContract,
        IQuestionsReadDbContext questionsReadDbContext)
    {
        _filesProvider = filesProvider;
        _tagsContract = tagsContract;
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

        var tags = await _tagsContract.GetByIds(new GetByIdsDto([.. questionTags]), cancellationToken);

        var questionsDto = questions.Select(q =>
            new QuestionDto(
                q.Id,
                q.Title,
                q.Text,
                q.UserId,
                (q.AttachmentId is not null ? filesDict[q.AttachmentId.Value] : null)!,
                q.Solution?.Id,
                tags.Select(t => t.Name),
                q.Status.ToRussianString()));

        return new QuestionResponse(questionsDto, count);
    }
}