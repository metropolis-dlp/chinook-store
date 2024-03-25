using AutoMapper;
using ChinookStore.Application._Common.Exceptions;
using ChinookStore.Application._Common.Extensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Entities;
using MediatR;

namespace ChinookStore.Application.Genres.Queries;

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