using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Genre : DomainEntity
{
    [MaxLength(200)]
    public string Name { get; protected set; }

    public Genre(string name)
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
