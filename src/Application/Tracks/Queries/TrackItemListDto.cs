using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Tracks.Queries;

public class TrackItemListDto : IMapFrom<Track>
{
    public required int Number { get; init; }
    public required string Name { get; init; }
    public required string Composer { get; init; }

    public required int Milliseconds { get; init; }
    public required decimal UnitPrice { get; init; }
    public required string MediaTypeName { get; init; }
}