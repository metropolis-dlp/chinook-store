using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Genres.Commands;

public record UpdateGenreCommand(int Id, string Name) : IRequest;

public class UpdateGenreCommandHandler(IRepository context) : IRequestHandler<UpdateGenreCommand>
{
    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await context.Query<Genre>().FirstByIdAsync(request.Id, cancellationToken);
        genre.Update(request.Name);
        await context.SaveChangesAsync(cancellationToken);
    }
}