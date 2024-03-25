using ChinookStore.Domain.Common;
using ChinookStore.Domain.ValueObjects;

namespace ChinookStore.Domain.Entities;

public class Invoice(DateTime date, decimal total, Address address) : DomainEntity
{
    public DateTime Date { get; } = date;
    public decimal Total { get; } = total;
    public Address Address { get; } = address;

    public required Customer Customer { get; init; }

    protected Invoice(DateTime date, decimal total)
        : this(date, total,
            new Address("", "", "", "", ""))
    {
    }
}
