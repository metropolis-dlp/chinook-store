using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using ChinookStore.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Features.Tracks.Commands;

public record UpdateTrackCommand(
    int Id, int Number, string Name, string Composer, int Milliseconds, decimal UnitPrice, int MediaTypeId)
    : IRequest;

public class UpdateTrackCommandValidator : AbstractValidator<UpdateTrackCommand>
{
    public UpdateTrackCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();
        RuleFor(v => v.Name).MaximumLength(200).NotNull().NotEmpty();

        RuleFor(v => v.Milliseconds).GreaterThan(0);
        RuleFor(v => v.UnitPrice).GreaterThan(0);
    }
}

public class UpdateTrackCommandHandler(IRepository repository) : IRequestHandler<UpdateTrackCommand>
{
    public async Task Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
    {
        var mediaTypes = await repository.EnumerateAsync<MediaType>(cancellationToken);

        var track = await repository.Query<Track>().FirstByIdAsync(request.Id, cancellationToken);
        track.Number = request.Number;
        track.Name = request.Name;
        track.Composer = request.Composer;
        track.Milliseconds = request.Milliseconds;
        track.UnitPrice = request.UnitPrice;
        track.MediaType = mediaTypes[request.MediaTypeId];

        await repository.SaveChangesAsync(cancellationToken);
    }
}
