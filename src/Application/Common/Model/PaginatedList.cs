using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Common.Model;

public class PaginatedList<T>(IEnumerable<T> items, int total)
{
  public IEnumerable<T> Items { get; } = items;
  public int Total { get; } = total;

  public static async Task<PaginatedList<TEntity>> CreateAsync<TEntity>(
      IQueryable<TEntity> source, int offset, int size, CancellationToken cancellationToken)
    where TEntity : class
  {
    var total = await source.CountAsync(cancellationToken);
    var items = await source.Skip(offset).Take(size)
      .ToListAsync(cancellationToken);

    return new PaginatedList<TEntity>(items, total);
  }

  public static async Task<PaginatedList<TDestination>> CreateAsync<TEntity,TDestination>(
        IQueryable<TEntity> source, int offset, int size, IConfigurationProvider configurationProvider,
        CancellationToken cancellationToken)
    where TEntity : class
  {
    var total = await source.CountAsync(cancellationToken);
    var items = await source
      .Skip(offset).Take(size)
      .ProjectTo<TDestination>(configurationProvider)
      .ToListAsync(cancellationToken);

    return new PaginatedList<TDestination>(items, total);
  }
}
