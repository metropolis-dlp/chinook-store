using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.ValueObjects;

public class Phone(string number) : ValueObject
{
    public string Number { get; } = number;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}