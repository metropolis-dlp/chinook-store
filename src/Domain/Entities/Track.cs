using ChinookStore.Domain.Common;
using ChinookStore.Domain.Enums;

namespace ChinookStore.Domain.Entities;

public class Track : DomainEntity
{
    public required int Number { get; set; }
    public required string Name { get; set; }
    public required string Composer { get; set; }

    public required int Milliseconds { get; set; }
    public required decimal UnitPrice { get; set; }

    public required Album Album { get; init; }
    public required MediaType MediaType { get; set; }
}
