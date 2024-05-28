using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Albums.Commands;
using ChinookStore.Application.Albums.Queries.GetAlbumById;
using ChinookStore.Application.Albums.Queries.GetAlbumsWithPagination;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/albums")]
public class AlbumsController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlbumDetailsDto>> GetAsync(int id)
    {
        return await Sender.Send(new GetAlbumByIdQuery(id));
    }

    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<AlbumListItemDto>>> GetWithPagination([FromQuery] GetAlbumsWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateAlbumCommand command)
    {
        var id = await Sender.Send(command);
        return CreatedAtAction("Get", id);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await Sender.Send(new DeleteAlbumCommand(id));
        return NoContent();
    }
}
