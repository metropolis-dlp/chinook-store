using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtists;

public record GetAllArtistsQuery() : IRequest<ArtistListItemDto[]>;
