using AutoMapper;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Albums.Queries.GetAlbumById;

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
