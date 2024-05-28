using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Artists.Commands;
using ChinookStore.Application.Artists.Queries.GetArtistById;
using ChinookStore.Application.Artists.Queries.GetArtists;
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

    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<ArtistListItemDto>>> GetWithPaginationAsync(
        [FromQuery] GetArtistsWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<ArtistListItemDto[]>> GetAllAsync()
    {
        return Ok(await Sender.Send(new GetAllArtistsQuery()));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateArtistCommand command)
    {
        var id = await Sender.Send(command);
        return CreatedAtAction("Get", id);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ArtistDetailsDto>> UpdateAsync(int id, UpdateArtistCommand command)
    {
        command.Id = id;
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
