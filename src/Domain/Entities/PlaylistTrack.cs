using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class PlaylistTrack : DomainEntity
{
    public required Playlist Playlist { get; init; }
    public required Track Track { get; init; }
}
