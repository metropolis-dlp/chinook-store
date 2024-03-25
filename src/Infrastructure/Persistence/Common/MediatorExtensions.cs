using ChinookStore.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Infrastructure.Persistence.Common;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context) 
    {
        var entities = context.ChangeTracker
            .Entries<DomainEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity).ToArray();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }

}