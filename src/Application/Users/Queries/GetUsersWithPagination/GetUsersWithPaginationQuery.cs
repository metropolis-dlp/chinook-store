using ChinookStore.Application._Common.Model;
using MediatR;

namespace ChinookStore.Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery
    : SortedPaginationQuery<UserListSortBy>,
      IRequest<PaginatedList<UserListItemDto>>
{
    public required string? Search { get; init; }
}
