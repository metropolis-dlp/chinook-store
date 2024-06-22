using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Features.Tracks.Commands;

public record DeleteTrackCommand(int Id) : IRequest;

public class DeleteTrackCommandHandler(IRepository repository) : IRequestHandler<DeleteTrackCommand>
{
    public async Task Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await repository.Query<Track>().FirstByIdAsync(request.Id, cancellationToken);
        repository.Delete(track);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
