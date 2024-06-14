using AutoMapper;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Application.Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Features.Artists.Queries.GetArtistById;

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
