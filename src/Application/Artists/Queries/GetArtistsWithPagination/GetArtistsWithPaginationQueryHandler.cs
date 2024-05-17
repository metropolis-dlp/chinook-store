using AutoMapper;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;

public class GetArtistsWithPaginationQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetArtistsWithPaginationQuery, PaginatedList<ArtistListItemDto>>
{
    public async Task<PaginatedList<ArtistListItemDto>> Handle(GetArtistsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = repository.Query<Artist>();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(a => a.Name.Contains(request.Search));
        }

        return await query
            .OrderBy(a => a.Name)
            .PaginatedListAsync<Artist, ArtistListItemDto>(
                request.Offset, request.Size, mapper.ConfigurationProvider, cancellationToken);
    }
}
