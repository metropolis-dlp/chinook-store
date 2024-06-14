using ChinookStore.Application.Features.MediaTypes.Queries;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/media-types")]
public class MediaTypesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MediaTypeListItemDto[]>> GetAsync()
    {
        return Ok(await Sender.Send(new GetAllMediaTypesQuery()));
    }
}
