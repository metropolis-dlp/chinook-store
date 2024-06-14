using ChinookStore.Application.Common.Model;
using ChinookStore.Application.Features.Artists.Commands;
using ChinookStore.Application.Features.Artists.Queries.GetArtistById;
using ChinookStore.Application.Features.Artists.Queries.GetArtists;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/artists")]
public class ArtistsController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ArtistDetailsDto>> GetAsync(int id)
    {
        return await Sender.Send(new GetArtistByIdQuery(id));
    }

    [HttpGet]
    public async Task<ActionResult<ArtistListItemDto[]>> GetAsync()
    {
        return await Sender.Send(new GetAllArtistsQuery());
    }

    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<ArtistListItemDto>>> GetWithPaginationAsync(
        [FromQuery] GetArtistsWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateArtistCommand command)
    {
        var id = await Sender.Send(command);
        return CreatedAtAction("Get", id);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateArtistCommand command)
    {
        command = command with { Id = id };
        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await Sender.Send(new DeleteArtistCommand(id));
        return NoContent();
    }
}
