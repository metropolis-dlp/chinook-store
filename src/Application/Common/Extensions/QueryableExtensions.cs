using ChinookStore.Application.Common.Exceptions;
using ChinookStore.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ChinookStore.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<TDomainObject> FirstByIdAsync<TDomainObject>(
          this IQueryable<TDomainObject> query, int id, CancellationToken cancellationToken)
      where TDomainObject : IdentityDomainObject
    {
        var entity = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(typeof(TDomainObject).Name, id);
        }

        return entity;
    }
}
