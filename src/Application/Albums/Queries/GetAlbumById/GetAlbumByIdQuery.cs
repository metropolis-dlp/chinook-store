using MediatR;

namespace ChinookStore.Application.Albums.Queries.GetAlbumById;

public record GetAlbumByIdQuery(int Id) : IRequest<AlbumDetailsDto>;
