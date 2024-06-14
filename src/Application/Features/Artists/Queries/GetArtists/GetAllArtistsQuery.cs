using MediatR;

namespace ChinookStore.Application.Features.Artists.Queries.GetArtists;

public record GetAllArtistsQuery() : IRequest<ArtistListItemDto[]>;
