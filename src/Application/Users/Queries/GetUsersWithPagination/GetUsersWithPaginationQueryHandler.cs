using AutoMapper;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryHandler(IRepository repository, IMapper mapper)
  : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserListItemDto>>
{
    public async Task<PaginatedList<UserListItemDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = repository.Query<User>();

        if (request.Search != null)
        {
            query = query.Where(u => u.FirstName.Contains(request.Search)
                                      || u.LastName.Contains(request.Search));
        }

        query = request.Sort switch
        {
            UserListSortBy.FirstName => request.Asc
                ? query.OrderBy(u => u.FirstName)
                : query.OrderByDescending(u => u.FirstName),
            UserListSortBy.LastName => request.Asc
                ? query.OrderBy(u => u.LastName)
                : query.OrderByDescending(u => u.LastName),
            UserListSortBy.Country => request.Asc
                ? query.OrderBy(u => u.Address.Country)
                : query.OrderByDescending(u => u.Address.Country),
            _ => request.Asc ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id)
        };

        return await query.PaginatedListAsync<User, UserListItemDto>(
          request.Offset, request.Size, mapper.ConfigurationProvider, cancellationToken);
    }
}
