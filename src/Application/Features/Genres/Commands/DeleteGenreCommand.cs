using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Features.Genres.Commands;

public record DeleteGenreCommand(int Id) : IRequest;

public class DeleteGenreCommandHandler(IRepository context) : IRequestHandler<DeleteGenreCommand>
{
    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await context.Query<Genre>().FirstByIdAsync(request.Id, cancellationToken);
        context.Delete(genre);
        await context.SaveChangesAsync(cancellationToken);
    }
}