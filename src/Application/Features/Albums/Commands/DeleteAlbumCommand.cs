using ChinookStore.Application.Common.Exceptions;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Albums.Commands;

public record DeleteAlbumCommand(int Id) : IRequest;

public class DeleteAlbumCommandHandler(IRepository repository) : IRequestHandler<DeleteAlbumCommand>
{
    public async Task Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await repository.Query<Album>().FirstByIdAsync(request.Id, cancellationToken);
        if (await repository.Query<Track>().AnyAsync(a => a.Album.Id == request.Id, cancellationToken))
        {
            throw new ValidationErrorException("There are one or more tracks related to this album.");
        }

        repository.Delete(album);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
