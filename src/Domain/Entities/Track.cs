using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;
using ChinookStore.Domain.Enums;

namespace ChinookStore.Domain.Entities;

public class Track(string name, string composer, int milliseconds, long bytes,  decimal unitPrice) : DomainEntity
{
    [MaxLength(200)]
    public string Name { get; } = name;
    [MaxLength(200)]
    public string Composer { get; } = composer;

    public int Milliseconds { get; } = milliseconds;
    public long Bytes { get; } = bytes;
    public decimal UnitPrice { get; } = unitPrice;

    public required Album Album { get; init; }
    public required MediaType MediaType { get; init; }
}
