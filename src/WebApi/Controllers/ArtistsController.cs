using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/artists")]
public class ArtistsController : ApiControllerBase
{
    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<ArtistListItemDto>>> GetWithPagination(int page, int size)
    {
        return await Sender.Send(new GetArtistsWithPaginationQuery(page, size));
    }
}
