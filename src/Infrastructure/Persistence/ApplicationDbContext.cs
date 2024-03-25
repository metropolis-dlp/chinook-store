using System.Reflection;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Entities;
using ChinookStore.Domain.Enums;
using ChinookStore.Infrastructure.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options, IMediator mediator)
    : BaseDbContext(options, mediator)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}