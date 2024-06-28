using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Features.Genres.Commands;

public record CreateGenreCommand(string Name) : IRequest<int>;

public class CreateGenreCommandHandler(IRepository context) : IRequestHandler<CreateGenreCommand, int>
{
    public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var created = context.Insert(new Genre(request.Name));
        await context.SaveChangesAsync(cancellationToken);

        return created.Id;
    }
}
