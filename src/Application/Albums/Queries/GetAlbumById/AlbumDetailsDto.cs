using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Albums.Queries.GetAlbumById;

public class AlbumDetailsDto : IMapFrom<Album>
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public int GenreId { get; init; }
    public int ArtistId { get; init; }
    public DateOnly ReleaseDate { get; init; }
}