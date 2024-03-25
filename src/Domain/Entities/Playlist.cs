using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Playlist(string name) : DomainEntity
{
    [MaxLength(120)]
    public string Name { get; } = name;
}
