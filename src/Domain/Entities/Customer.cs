using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Customer : DomainEntity
{
    public required User User { get; init; }
    public required Employee SupportRep { get; init; }
}
