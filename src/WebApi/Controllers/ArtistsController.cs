using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Artists.Commands;
using ChinookStore.Application.Artists.Queries.GetArtistById;
using ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/artists")]
public class ArtistsController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ArtistDetailsDto>> Get(int id)
    {
        return await Sender.Send(new GetArtistByIdQuery(id));
    }

    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<ArtistListItemDto>>> GetWithPagination([FromQuery] GetArtistsWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<ArtistDetailsDto>> Create(CreateArtistCommand command)
    {
        await Sender.Send(command);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ArtistDetailsDto>> Update(int id, UpdateArtistCommand command)
    {
        command.Id = id;
        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Sender.Send(new DeleteArtistCommand(id));
        return NoContent();
    }
}
