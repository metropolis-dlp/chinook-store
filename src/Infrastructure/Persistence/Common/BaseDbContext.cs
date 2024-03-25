using System.Data;
using System.Linq.Expressions;
using ChinookStore.Application._Common.Interfaces;
using ChinookStore.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChinookStore.Infrastructure.Persistence.Common;

public abstract class BaseDbContext(DbContextOptions options, IMediator mediator)
    : DbContext(options), IRepository
{
    private IDbContextTransaction? _currentTransaction;

    public IQueryable<T> Query<T>()
        where T : DomainEntity
    {
        return Set<T>();
    }

    public void Insert<T>(T entity)
        where T : DomainEntity
    {
        Add(entity);
    }

    public void Delete<T>(T entity)
        where T : DomainEntity
    {
        Remove(entity);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);
        var enumEntries = ChangeTracker.Entries().Where(e =>
            e.Entity.GetType().IsAssignableTo(typeof(Enumeration)) && e.State == EntityState.Added);
        foreach (var enumEntry in enumEntries)
        {
            enumEntry.State = EntityState.Unchanged;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public bool HasActiveTransaction => _currentTransaction != null;
    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;
        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return _currentTransaction;
    }
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
    protected void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}