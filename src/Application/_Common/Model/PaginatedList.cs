using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application._Common.Model;

public class PaginatedList<T>(IEnumerable<T> items, int count, int page, int pageSize)
{
  public IEnumerable<T> Items { get; } = items;
  public int Page { get; } = page;
  public int Total { get; } = count;
  public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);
  public bool HasPreviousPage => Page > 0;
  public bool HasNextPage => Page < TotalPages - 1;

  public static async Task<PaginatedList<TEntity>> CreateAsync<TEntity>(IQueryable<TEntity> source, int pageIndex, int pageSize, CancellationToken cancellationToken)
    where TEntity : class
  {
    var total = await source.CountAsync(cancellationToken);
    var items = await source.Skip(pageIndex * pageSize).Take(pageSize)
      .ToListAsync(cancellationToken);

    return new PaginatedList<TEntity>(items, total, pageIndex, pageSize);
  }

  public static async Task<PaginatedList<TDestination>> CreateAsync<TEntity,TDestination>(IQueryable<TEntity> source,
      int pageIndex, int pageSize, IConfigurationProvider configurationProvider, CancellationToken cancellationToken)
    where TEntity : class
  {
    var total = await source.CountAsync(cancellationToken);
    var items = await source
      .Skip(pageIndex * pageSize).Take(pageSize)
      .ProjectTo<TDestination>(configurationProvider)
      .ToListAsync(cancellationToken);

    return new PaginatedList<TDestination>(items, total, pageIndex, pageSize);
  }
}
