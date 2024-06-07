using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ChinookStore.Application.Albums.Commands;

public record UpdateAlbumCommand(int Id, string Title, int ArtistId, int GenreId, DateOnly ReleaseDate) : IRequest;

public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
{
    public UpdateAlbumCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(400).NotNull().NotEmpty();
    }
}

public class UpdateAlbumCommandHandler(IRepository repository) : IRequestHandler<UpdateAlbumCommand>
{
    public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        var artist = await repository.Query<Artist>().FirstByIdAsync(request.ArtistId, cancellationToken);
        var genre = await repository.Query<Genre>().FirstByIdAsync(request.GenreId, cancellationToken);

        var album = await repository.Query<Album>().FirstByIdAsync(request.Id, cancellationToken);
        album.Title = request.Title;
        album.ReleaseDate = request.ReleaseDate;
        album.Artist = artist;
        album.Genre = genre;

        await repository.SaveChangesAsync(cancellationToken);
    }
}
