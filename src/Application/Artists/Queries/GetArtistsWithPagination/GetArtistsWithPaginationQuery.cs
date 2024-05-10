using ChinookStore.Application._Common.Model;
using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;

public class GetArtistsWithPaginationQuery(string? search, int pageIndex, int pageSize)
    : PaginationQuery(pageIndex, pageSize), IRequest<PaginatedList<ArtistListItemDto>>
{
    public string? Search { get; } = search;
}
