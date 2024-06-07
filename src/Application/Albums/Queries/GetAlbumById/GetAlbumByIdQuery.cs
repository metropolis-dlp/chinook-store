using AutoMapper;
using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Albums.Queries.GetAlbumById;

public record GetAlbumByIdQuery(int Id) : IRequest<AlbumDetailsDto>;

public class GetAlbumByIdQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetAlbumByIdQuery, AlbumDetailsDto>
{
    public async Task<AlbumDetailsDto> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<AlbumDetailsDto>(
            await repository.Query<Album>()
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstByIdAsync(request.Id, cancellationToken));
    }
}
