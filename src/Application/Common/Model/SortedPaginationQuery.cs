namespace ChinookStore.Application.Common.Model;

public class SortedPaginationQuery<TSort> : PaginationQuery
  where TSort : Enum
{
  public required TSort Sort { get; init; }
  public required bool Asc { get; init; }
}
