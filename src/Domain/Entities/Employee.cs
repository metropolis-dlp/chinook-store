using System.ComponentModel.DataAnnotations;
using ChinookStore.Domain.Common;

namespace ChinookStore.Domain.Entities;

public class Employee(string title, DateOnly birthDate, DateOnly hireDate) : DomainEntity
{
    [MaxLength(200)]
    public string Title { get; } = title;

    public DateOnly BirthDate { get; } = birthDate;
    public DateOnly HireDate { get; } = hireDate;

    public required User User { get; init; }
    public Employee? ReportsTo { get; protected set; }

    public void SetManager(Employee manager)
    {
        ReportsTo = manager;
    }
}
