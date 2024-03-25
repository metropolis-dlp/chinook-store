using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Album(string title) : DomainEntity
{
    [MaxLength(300)]
    public string Title { get; } = title;
    public required Artist Artist { get; init; }
}
