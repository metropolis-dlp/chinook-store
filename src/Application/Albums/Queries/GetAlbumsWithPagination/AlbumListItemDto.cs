using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Albums.Queries.GetAlbumsWithPagination;

public class AlbumListItemDto : IMapFrom<Album>
{
    public required string Title { get; init; }
    public DateOnly ReleaseDate { get; init; }

    public required string ArtistName { get; init; }
    public required string GenreName { get; init; }
}
