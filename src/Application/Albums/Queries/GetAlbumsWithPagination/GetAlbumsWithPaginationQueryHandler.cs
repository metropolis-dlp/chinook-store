using AutoMapper;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Albums.Queries.GetAlbumsWithPagination;

public class GetAlbumsWithPaginationQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetAlbumsWithPaginationQuery, PaginatedList<AlbumListItemDto>>
{
    public async Task<PaginatedList<AlbumListItemDto>> Handle(GetAlbumsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = repository.Query<Album>();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(a => a.Title.Contains(request.Search) || a.Artist.Name.Contains(request.Search));
        }

        query = request.Sort switch
        {
            AlbumListSortBy.Title => request.Asc
                ? query.OrderBy(a => a.Title)
                : query.OrderByDescending(a => a.Title),
            AlbumListSortBy.Artist => request.Asc
                ? query.OrderBy(a => a.Artist.Name)
                : query.OrderByDescending(a => a.Artist.Name),
            AlbumListSortBy.Genre => request.Asc
                ? query.OrderBy(a => a.Genre.Name)
                : query.OrderByDescending(a => a.Genre.Name),
            AlbumListSortBy.Release => request.Asc
                ? query.OrderBy(a => a.ReleaseDate)
                : query.OrderByDescending(a => a.ReleaseDate),
            _ => request.Asc ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id)
        };

        return await query
            .PaginatedListAsync<Album, AlbumListItemDto>(
                request.Offset, request.Size, mapper.ConfigurationProvider, cancellationToken);
    }
}
