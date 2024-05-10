using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Artists.Commands;

public record CreateArtistCommand(string Name) : IRequest;

public class CreateArtistCommandHandler(IRepository repository) : IRequestHandler<CreateArtistCommand>
{
    public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        repository.Insert(new Artist(request.Name));
        await repository.SaveChangesAsync(cancellationToken);
    }
}
