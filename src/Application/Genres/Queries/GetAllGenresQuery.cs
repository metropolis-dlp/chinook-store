using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Genres.Queries;

public class GenresListItemDto : BasicItemDto, IMapFrom<Genre>;

public record GetAllGenresQuery : IRequest<GenresListItemDto[]>;

public class GetAllGenresQueryHandler(IMapper mapper, IRepository context)
    : IRequestHandler<GetAllGenresQuery, GenresListItemDto[]>
{
    public async Task<GenresListItemDto[]> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await context.Query<Genre>()
            .ProjectTo<GenresListItemDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}