using ChinookStore.Application.Common.Model;
using MediatR;

namespace ChinookStore.Application.Features.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery
    : SortedPaginationQuery<UserListSortBy>,
      IRequest<PaginatedList<UserListItemDto>>
{
    public required string? Search { get; init; }
}
