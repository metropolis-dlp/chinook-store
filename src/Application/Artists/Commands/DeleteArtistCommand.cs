using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Artists.Commands;

public record DeleteArtistCommand(int Id) : IRequest;

public class DeleteArtistCommandHandler(IRepository repository) : IRequestHandler<DeleteArtistCommand>
{
    public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await repository.Query<Artist>().FirstByIdAsync(request.Id, cancellationToken);
        repository.Delete(artist);

        await repository.SaveChangesAsync(cancellationToken);
    }
}
