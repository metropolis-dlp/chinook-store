using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using ChinookStore.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Features.Tracks.Commands;

public record CreateTrackCommand(
        int AlbumId, int Number, string Name, string Composer, int Milliseconds, decimal UnitPrice, int MediaTypeId)
    : IRequest<int>;

public class CreateTrackCommandValidator : AbstractValidator<CreateTrackCommand>
{
    public CreateTrackCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();

        RuleFor(v => v.Milliseconds).GreaterThan(0);
        RuleFor(v => v.UnitPrice).GreaterThan(0);
    }
}

public class CreateTrackCommandHandler(IRepository repository) : IRequestHandler<CreateTrackCommand, int>
{
    public async Task<int> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
    {
        var album = await repository.Query<Album>().FirstByIdAsync(request.AlbumId, cancellationToken);
        var mediaTypes = await repository.EnumerateAsync<MediaType>(cancellationToken);

        var entry = repository.Insert(new Track
        {
            Album = album,
            Number = request.Number,
            Name = request.Name,
            Composer = request.Composer,
            Milliseconds = request.Milliseconds,
            UnitPrice = request.UnitPrice,
            MediaType = mediaTypes[request.MediaTypeId]
        });

        await repository.SaveChangesAsync(cancellationToken);
        return entry.Id;
    }
}
