using AutoMapper;
using ChinookStore.Application._Common.Model;
using ChinookStore.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application._Common.Mappings;

public static class MappingExtensions
{
  public static Task<PaginatedList<TDomainObject>> PaginatedListAsync<TDomainObject>(
    this IQueryable<TDomainObject> queryable,
    int pageIndex, int pageSize,
    CancellationToken cancellationToken)
      where TDomainObject : IdentityDomainObject
    => PaginatedList<TDomainObject>.CreateAsync(queryable.AsNoTracking(), pageIndex, pageSize, cancellationToken);

  public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDomainObject, TDestination>(
    this IQueryable<TDomainObject> queryable,
    int pageIndex, int pageSize, IConfigurationProvider configurationProvider,
    CancellationToken cancellationToken)
      where TDestination : class
      where TDomainObject: IdentityDomainObject
    => PaginatedList<TDestination>.CreateAsync<TDomainObject, TDestination>(queryable.AsNoTracking(), pageIndex, pageSize, configurationProvider, cancellationToken);
}
