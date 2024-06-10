using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Tracks.Queries;

public record GetTracksByAlbumQuery(int AlbumId) : IRequest<TrackItemListDto[]>;

public class GetTracksByAlbumQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetTracksByAlbumQuery, TrackItemListDto[]>
{
    public async Task<TrackItemListDto[]> Handle(GetTracksByAlbumQuery request, CancellationToken cancellationToken)
    {
        return await repository.Query<Track>()
            .Where(t => t.Album.Id == request.AlbumId)
            .ProjectTo<TrackItemListDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}
