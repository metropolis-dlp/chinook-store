using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Albums.Queries.GetAlbumsWithPagination;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/albums")]
public class AlbumsController : ApiControllerBase
{
    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<AlbumListItemDto>>> GetWithPagination([FromQuery] GetAlbumsWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }
}
