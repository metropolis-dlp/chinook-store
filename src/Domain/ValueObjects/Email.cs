using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.ValueObjects;

public class Email(string address) : ValueObject
{
    public string Address { get; } = address;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}