using ChinookStore.Application.Common.Model;
using ChinookStore.Application.Features.Users.Queries.GetUsersWithPagination;
using ChinookStore.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/users")]
public class UsersController : ApiControllerBase
{
    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<UserListItemDto>>> GetWithPagination(
        [FromQuery] GetUsersWithPaginationQuery query)
    {
        return await Sender.Send(query);
    }
}
