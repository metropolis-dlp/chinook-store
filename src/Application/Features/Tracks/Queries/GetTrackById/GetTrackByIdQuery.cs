using AutoMapper;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Tracks.Queries.GetTrackById;

public record GetTrackByIdQuery(int Id) : IRequest<TrackDetailsDto>;

public class GetTrackByIdQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetTrackByIdQuery, TrackDetailsDto>
{
    public async Task<TrackDetailsDto> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<TrackDetailsDto>(
            await repository.Query<Track>()
                .Include(t => t.MediaType)
                .FirstByIdAsync(request.Id, cancellationToken));
    }
}
