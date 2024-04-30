using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Genre(string name) : DomainEntity
{
    [MaxLength(200)]
    public string Name { get; protected set; } = name;

    public void Update(string name)
    {
        Name = name;
    }
}
