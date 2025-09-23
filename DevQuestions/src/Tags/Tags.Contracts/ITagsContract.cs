using Tags.Contracts.Dtos;

namespace Tags.Contracts;

public interface ITagsContract
{
    Task CreateTag(CreateTagDto dto, CancellationToken cancellationToken);

    Task<IReadOnlyList<TagDto>> GetByIds(GetByIdsDto dto, CancellationToken cancellationToken);
}
