using ChinookStore.Domain.Common;
using ChinookStore.Domain.Enums;

namespace ChinookStore.Domain.Entities;

public class Track : DomainEntity
{
    public required int Number { get; init; }
    public required string Name { get; init; }
    public required string Composer { get; init; }

    public required int Milliseconds { get; init; }
    public required decimal UnitPrice { get; init; }

    public required Album Album { get; init; }
    public required MediaType MediaType { get; init; }
}
