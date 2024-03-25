using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Employees.Queries.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<EmployeeListItemDto[]>;

public class GetAllEmployeesQueryHandler(IRepository repository, IMapper mapper)
    : IRequestHandler<GetAllEmployeesQuery, EmployeeListItemDto[]>
{
    public async Task<EmployeeListItemDto[]> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await repository.Query<Employee>()
            .ProjectTo<EmployeeListItemDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}
