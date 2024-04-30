using AutoMapper;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;

public class ArtistListItemDto : IMapFrom<Artist>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
