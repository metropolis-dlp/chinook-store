using ChinookStore.Application._Common.Model;
using MediatR;

namespace ChinookStore.Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery(int pageIndex, int pageSize, UserListSortBy sortBy, bool isAscending)
    : SortedPaginationQuery<UserListSortBy>(pageIndex, pageSize, sortBy, isAscending),
      IRequest<PaginatedList<UserListItemDto>>
{
    public string? SearchText { get; init; }
}
