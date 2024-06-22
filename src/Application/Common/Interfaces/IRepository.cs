using System.Collections;
using ChinookStore.Domain.Common;

namespace ChinookStore.Application.Common.Interfaces;

public interface IRepository
{
    IQueryable<TEntity> Query<TEntity>()
        where TEntity : DomainEntity;

    TEntity Insert<TEntity>(TEntity entity)
        where TEntity : DomainEntity;

    void Delete<TEntity>(TEntity entity)
        where TEntity : DomainEntity;

    Task<IDictionary<int, TEnumeration>> EnumerateAsync<TEnumeration>(CancellationToken cancellationToken)
        where TEnumeration : Enumeration;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
