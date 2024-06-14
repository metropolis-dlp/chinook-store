using ChinookStore.Application.Common.Exceptions;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Artists.Commands;

public record DeleteArtistCommand(int Id) : IRequest;

public class DeleteArtistCommandHandler(IRepository repository) : IRequestHandler<DeleteArtistCommand>
{
    public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await repository.Query<Artist>().FirstByIdAsync(request.Id, cancellationToken);
        if (await repository.Query<Album>().AnyAsync(a => a.Artist.Id == request.Id, cancellationToken))
        {
            throw new ValidationErrorException("There are one or more albums related to this artist.");
        }

        repository.Delete(artist);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
