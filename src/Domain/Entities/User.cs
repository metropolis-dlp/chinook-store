using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;
using ChinookStore.Domain.ValueObjects;

namespace ChinookStore.Domain.Entities;

public class User(string firstName, string lastName, Address address, Email email, Phone phone) : DomainEntity
{
    [MaxLength(200)]
    public string FirstName { get; } = firstName;

    [MaxLength(200)]
    public string LastName { get; } = lastName;

    public Address Address { get; init; } = address;
    public Phone Phone { get; init; } = phone;
    public Email Email { get; init; } = email;

    protected User(string firstName, string lastName)
        : this(firstName, lastName,
            new Address("", "", "", "", ""),
            new Email(""), new Phone(""))
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
