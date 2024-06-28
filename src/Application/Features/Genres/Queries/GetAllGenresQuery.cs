using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Application.Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.Genres.Queries;

public class GenresListItemDto : BasicItemDto, IMapFrom<Genre>;

public record GetAllGenresQuery : IRequest<GenresListItemDto[]>;

public class GetAllGenresQueryHandler(IMapper mapper, IRepository context)
    : IRequestHandler<GetAllGenresQuery, GenresListItemDto[]>
{
    public async Task<GenresListItemDto[]> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await context.Query<Genre>()
            .OrderBy(g => g.Name)
            .ProjectTo<GenresListItemDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}
