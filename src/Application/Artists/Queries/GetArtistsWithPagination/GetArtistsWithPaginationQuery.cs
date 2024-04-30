using AutoMapper;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Artists.Queries.GetArtistsWithPagination;

public class GetArtistsWithPaginationQuery(int pageIndex, int pageSize)
    : PaginationQuery(pageIndex, pageSize), IRequest<PaginatedList<ArtistListItemDto>>;

public class GetArtistsWithPaginationQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetArtistsWithPaginationQuery, PaginatedList<ArtistListItemDto>>
{
    public async Task<PaginatedList<ArtistListItemDto>> Handle(GetArtistsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await repository.Query<Artist>()
            .OrderBy(a => a.Name)
            .PaginatedListAsync<Artist, ArtistListItemDto>(
                request.PageIndex, request.PageSize, mapper.ConfigurationProvider, cancellationToken);
    }
}
