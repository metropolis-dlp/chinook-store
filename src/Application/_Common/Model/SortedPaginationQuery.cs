namespace ChinookStore.Application._Common.Model;

public class SortedPaginationQuery<TOrder> : PaginationQuery
  where TOrder : Enum
{
  public required TOrder Order { get; init; }
  public required bool Asc { get; init; }
}
