using AutoMapper;
using ChinookStore.Application._Common.Mappings;
using ChinookStore.Domain.Entities;

namespace ChinookStore.Application.Users.Queries.GetUsersWithPagination;

public class UserListItemDto : IMapFrom<User>
{
    public required int Id { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init;  }
    public required string Address { get; init; }
    public required string Country { get; init; }

    public required string PhoneNumber { get; init; }
    public required string EmailAddress { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserListItemDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address.Street + ", " + s.Address.City))
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country));
    }
}
