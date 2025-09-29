using Shared.Abstractions;
using Tags.Contracts;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Features;

namespace Tags.Presenters;

public class TagsContract : ITagsContract
{
    private readonly IQueryHandler<GetByIds.GetByIdsQuery, IReadOnlyList<TagDto>> _handler;
    private readonly TagsDbContext _dbContext;

    public TagsContract(
        IQueryHandler<GetByIds.GetByIdsQuery, IReadOnlyList<TagDto>> handler,
        TagsDbContext dbContext)
    {
        _handler = handler;
        _dbContext = dbContext;
    }

    public async Task CreateTag(CreateTagDto dto, CancellationToken cancellationToken)
    {
        await Create.HandleAsync(dto, _dbContext, cancellationToken);
    }

    public async Task<IReadOnlyList<TagDto>> GetByIds(GetByIdsDto dto, CancellationToken cancellationToken)
    {
        return await _handler.HandleAsync(new GetByIds.GetByIdsQuery(dto), cancellationToken);
    }
}