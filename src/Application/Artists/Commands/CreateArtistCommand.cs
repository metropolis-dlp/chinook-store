using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Artists.Commands;

public record CreateArtistCommand(string Name) : IRequest;

public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();
    }
}

public class CreateArtistCommandHandler(IRepository repository) : IRequestHandler<CreateArtistCommand>
{
    public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        repository.Insert(new Artist(request.Name));
        await repository.SaveChangesAsync(cancellationToken);
    }
}
