using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Artist(string name) : DomainEntity
{
    [MaxLength(200)]
    public string Name { get; } = name;
}
