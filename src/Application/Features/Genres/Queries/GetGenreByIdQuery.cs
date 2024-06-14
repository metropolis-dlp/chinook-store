using AutoMapper;
using ChinookStore.Application.Common.Extensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Application.Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Features.Genres.Queries;

public class GenreDto : BasicItemDto, IMapFrom<Genre>;

public record GetGenreByIdQuery(int Id) : IRequest<GenreDto>;

public class GetGenreByIdQueryHandler(IMapper mapper, IRepository context)
    : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await context.Query<Genre>().FirstByIdAsync(request.Id, cancellationToken);
        return mapper.Map<GenreDto>(genre);
    }
}