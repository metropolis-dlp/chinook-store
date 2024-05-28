using AutoMapper;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Artists.Queries.GetArtists;

public class ArtistListItemDto : IMapFrom<Artist>
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int Albums { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Artist, ArtistListItemDto>()
            .ForMember(d => d.Albums, opt => opt.MapFrom(s => s.Albums.Count()));
    }
}
