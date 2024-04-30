using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Album(string title, DateOnly releaseDate) : DomainEntity
{
    [MaxLength(300)]
    public string Title { get; } = title;

    public DateOnly ReleaseDate { get; } = releaseDate;
    public required Artist Artist { get; init; }
    public required Genre Genre { get; init; }
}
