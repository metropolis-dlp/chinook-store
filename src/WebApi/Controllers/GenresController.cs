using ChinookStore.Application.Features.Genres.Commands;
using ChinookStore.Application.Features.Genres.Queries;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/genres")]
public class GenresController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GenresListItemDto[]>> GetAsync()
    {
        return Ok(await Sender.Send(new GetAllGenresQuery()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDto>> GetAsync(int id)
    {
        return Ok(await Sender.Send(new GetGenreByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateGenreCommand command)
    {
        var id = await Sender.Send(command);
        return CreatedAtAction("Get", id);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateGenreCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Invalid Id");
        }

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await Sender.Send(new DeleteGenreCommand(id));
        return NoContent();
    }
}
