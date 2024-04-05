using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.ValueObjects;

public class Address(string street, string city, string state, string country, string postalCode)
    : ValueObject
{
    [MaxLength(40)]
    public string Street { get; } = street;

    [MaxLength(40)]
    public string City { get; } = city;

    [MaxLength(40)]
    public string State { get; } = state;

    [MaxLength(40)]
    public string Country { get; } = country;

    [MaxLength(10)]
    public string PostalCode { get; } = postalCode;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return PostalCode;
    }
}
