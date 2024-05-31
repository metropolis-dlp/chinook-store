using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Album : DomainEntity
{
    [MaxLength(300)]
    public required string Title { get; set; }

    public required DateOnly ReleaseDate { get; set; }
    public required Artist Artist { get; set; }
    public required Genre Genre { get; set; }
}
