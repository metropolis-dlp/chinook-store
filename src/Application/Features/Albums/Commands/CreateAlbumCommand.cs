using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Features.Albums.Commands;

public record CreateAlbumCommand(string Title, int ArtistId, int GenreId, DateOnly ReleaseDate
) : IRequest<int>;

public class CreateAlbumCCommandValidator : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumCCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(400).NotNull().NotEmpty();
    }
}

public class CreateAlbumCommandHandler(IRepository repository) : IRequestHandler<CreateAlbumCommand, int>
{
    public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        var artist = await repository.Query<Artist>().FirstByIdAsync(request.ArtistId, cancellationToken);
        var genre = await repository.Query<Genre>().FirstByIdAsync(request.GenreId, cancellationToken);

        var entry = repository.Insert(new Album
        {
            Title = request.Title,
            ReleaseDate = request.ReleaseDate,
            Artist = artist,
            Genre = genre
        });

        await repository.SaveChangesAsync(cancellationToken);
        return entry.Id;
    }
}
