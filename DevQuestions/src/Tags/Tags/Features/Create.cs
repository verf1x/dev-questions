using Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tags.Contracts;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Domain;

namespace Tags.Features;

public sealed class Create
{

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("tags", HandleAsync);
        }
    }

    private static async Task<IResult> HandleAsync(
        CreateTagDto dto,
        TagsDbContext tagsDbContext,
        CancellationToken cancellationToken)
    {
        var tag = new Tag { Name = dto.Name, };

        await tagsDbContext.AddAsync(tag, cancellationToken);

        return Results.Ok(tag.Id);
    }
}