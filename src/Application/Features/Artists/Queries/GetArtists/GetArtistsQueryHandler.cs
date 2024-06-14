using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Application.Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Artists.Queries.GetArtists;

public class GetArtistsQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetArtistsWithPaginationQuery, PaginatedList<ArtistListItemDto>>,
      IRequestHandler<GetAllArtistsQuery, ArtistListItemDto[]>
{
    public async Task<PaginatedList<ArtistListItemDto>> Handle(GetArtistsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = repository.Query<Artist>();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(a => a.Name.Contains(request.Search));
        }

        query = request.Sort switch
        {
            ArtistListSortBy.Name => request.Asc
                ? query.OrderBy(a => a.Name)
                : query.OrderByDescending(a => a.Name),
            ArtistListSortBy.Albums => request.Asc
                ? query.OrderBy(a => a.Albums.Count())
                : query.OrderByDescending(a => a.Albums.Count()),
            _ => request.Asc ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id)
        };

        return await query
            .PaginatedListAsync<Artist, ArtistListItemDto>(
                request.Offset, request.Size, mapper.ConfigurationProvider, cancellationToken);
    }

    public async Task<ArtistListItemDto[]> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        return await repository.Query<Artist>()
            .ProjectTo<ArtistListItemDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}
