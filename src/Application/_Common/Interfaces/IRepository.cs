using ChinookStore.Domain.Common;

namespace ChinookStore.Application._Common.Interfaces;

public interface IRepository
{
    IQueryable<TEntity> Query<TEntity>()
        where TEntity : DomainEntity;

    void Insert<TEntity>(TEntity entity)
        where TEntity : DomainEntity;

    void Delete<TEntity>(TEntity entity)
        where TEntity : DomainEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
