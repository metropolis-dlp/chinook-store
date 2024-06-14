using AutoMapper;
using ChinookStore.Application.Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Features.Employees.Queries.GetAllEmployees;

public class EmployeeListItemDto : IMapFrom<Employee>
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Title { get; init; }
    public DateOnly HireDate { get; init; }

    public required string ReportsTo { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeListItemDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
            .ForMember(d => d.ReportsTo,
                opt => opt.MapFrom(s => s.ReportsTo != null
                    ? s.ReportsTo.User.FirstName + " " + s.ReportsTo.User.LastName
                    : ""));
    }
}
