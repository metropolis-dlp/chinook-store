using ChinookStore.Application.Features.Tracks.Queries;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/tracks")]
public class TracksController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TrackItemListDto[]>> Get(int albumId)
    {
        return await Sender.Send(new GetTracksByAlbumQuery(albumId));
    }
}
