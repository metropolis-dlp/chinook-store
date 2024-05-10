using AutoMapper;
using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtistById;

public class ArtistDetailsDto : BasicItemDto, IMapFrom<Artist> { }

public record GetArtistByIdQuery(int Id) : IRequest<ArtistDetailsDto>;

public class GetArtistByIdQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetArtistByIdQuery, ArtistDetailsDto>
{
    public async Task<ArtistDetailsDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<ArtistDetailsDto>(await repository.Query<Artist>()
            .FirstByIdAsync(request.Id, cancellationToken));
    }
}
