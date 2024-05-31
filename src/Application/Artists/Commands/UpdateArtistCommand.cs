using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Artists.Commands;

public record UpdateArtistCommand(int Id, string Name) : IRequest;

public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();
    }
}

public class UpdateArtistCommandHandler(IRepository repository) : IRequestHandler<UpdateArtistCommand>
{
    public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await repository.Query<Artist>().FirstByIdAsync(request.Id, cancellationToken);
        artist.Name = request.Name;

        await repository.SaveChangesAsync(cancellationToken);
    }
}
