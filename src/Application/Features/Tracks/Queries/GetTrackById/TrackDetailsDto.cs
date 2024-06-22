using ChinookStore.Application.Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Features.Tracks.Queries.GetTrackById;

public class TrackDetailsDto : IMapFrom<Track>
{
    public required int AlbumId { get; init; }

    public required int Number { get; init; }
    public required string Name { get; init; }
    public required string Composer { get; init; }

    public required int Milliseconds { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int MediaTypeId { get; init; }
}
