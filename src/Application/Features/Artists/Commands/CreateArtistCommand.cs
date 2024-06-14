using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Features.Artists.Commands;

public record CreateArtistCommand(string Name) : IRequest<int>;

public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();
    }
}

public class CreateArtistCommandHandler(IRepository repository) : IRequestHandler<CreateArtistCommand, int>
{
    public async Task<int> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var entry = repository.Insert(new Artist(request.Name));
        await repository.SaveChangesAsync(cancellationToken);

        return entry.Id;
    }
}
