using ChinookStore.Application.Features.Tracks.Commands;
using ChinookStore.Application.Features.Tracks.Queries;
using ChinookStore.Application.Features.Tracks.Queries.GetTrackById;
using ChinookStore.Application.Features.Tracks.Queries.GetTracksByAlbum;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/tracks")]
public class TracksController : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrackDetailsDto>> GetAsync(int id)
    {
        return await Sender.Send(new GetTrackByIdQuery(id));
    }

    [HttpGet]
    public async Task<ActionResult<TrackItemListDto[]>> GetByAlbumAsync(int albumId)
    {
        return await Sender.Send(new GetTracksByAlbumQuery(albumId));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateTrackCommand command)
    {
        var id = await Sender.Send(command);
        return CreatedAtAction("GetByAlbum", id);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateTrackCommand command)
    {
        command = command with { Id = id };
        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await Sender.Send(new DeleteTrackCommand(id));
        return NoContent();
    }
}
