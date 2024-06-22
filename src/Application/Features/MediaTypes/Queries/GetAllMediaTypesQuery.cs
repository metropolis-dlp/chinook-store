using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application.Common.Interfaces;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Application.Common.Model;
using ChinookStore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Features.MediaTypes.Queries;

public class MediaTypeListItemDto : BasicItemDto, IMapFrom<MediaType>;

public class GetAllMediaTypesQuery : IRequest<MediaTypeListItemDto[]>;

public class GetAllMediaTypesQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetAllMediaTypesQuery, MediaTypeListItemDto[]>
{
    public async Task<MediaTypeListItemDto[]> Handle(GetAllMediaTypesQuery request, CancellationToken cancellationToken)
    {
        var mediaTypes = await repository.EnumerateAsync<MediaType>(cancellationToken);
        return mapper.Map<MediaTypeListItemDto[]>(mediaTypes.Values);
    }
}
