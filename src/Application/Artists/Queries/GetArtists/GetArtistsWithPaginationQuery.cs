using ChinookStore.Application._Common.Model;
using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtists;

public class GetArtistsWithPaginationQuery
    : SortedPaginationQuery<ArtistListSortBy>, IRequest<PaginatedList<ArtistListItemDto>>
{
    public required string? Search { get; init; }
}
