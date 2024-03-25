using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Genres.Commands;

public record CreateGenreCommand(string Name) : IRequest;

public class CreateGenreCommandHandler(IRepository context) : IRequestHandler<CreateGenreCommand>
{
    public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        context.Insert(new Genre(request.Name));
        await context.SaveChangesAsync(cancellationToken);
    }
}
