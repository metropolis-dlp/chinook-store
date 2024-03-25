using ChinookStore.Application._Common.Model;
using ChinookStore.Application.Users.Queries.GetUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/users")]
public class UsersController : ApiControllerBase
{
    [HttpGet("query")]
    public async Task<ActionResult<PaginatedList<UserListItemDto>>> GetWithPagination(
        string? search, int page, int size, UserListSortBy sort, bool asc)
    {
        return await Sender.Send(new GetUsersWithPaginationQuery(page, size, sort, asc)
        {
            SearchText = search
        });
    }
}
