using ChinookStore.Application._Common.Model;
using MediatR;

namespace ChinookStore.Application.Albums.Queries.GetAlbumsWithPagination;

public class GetAlbumsWithPaginationQuery
    : SortedPaginationQuery<AlbumListSortBy>, IRequest<PaginatedList<AlbumListItemDto>>
{
    public required string? Search { get; init; }
    public required int? ArtistId { get; init; }
}
