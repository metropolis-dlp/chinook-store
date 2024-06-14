using ChinookStore.Application.Common.Model;
using MediatR;

namespace ChinookStore.Application.Features.Artists.Queries.GetArtists;

public class GetArtistsWithPaginationQuery
    : SortedPaginationQuery<ArtistListSortBy>, IRequest<PaginatedList<ArtistListItemDto>>
{
    public required string? Search { get; init; }
}
