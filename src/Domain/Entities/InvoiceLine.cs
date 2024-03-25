using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class InvoiceLine(decimal unitPrice, int quantity) : DomainEntity
{
    public decimal UnitPrice { get; } = unitPrice;
    public int Quantity { get; } = quantity;

    public required Invoice Invoice { get; init; }
    public required Track Track { get; init; }
}
