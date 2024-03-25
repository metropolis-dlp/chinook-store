namespace ChinookStore.Application._Common.Model;

public class SortedPaginationQuery<TOrder> : PaginationQuery
  where TOrder : Enum
{
  public TOrder Order { get; } = default!;
  public bool Asc { get; } = true;

  public SortedPaginationQuery(int pageIndex, int pageSize, TOrder order, bool asc)
    : base(pageIndex, pageSize)
  {
    Order = order;
    Asc = asc;
  }

  public SortedPaginationQuery()
  {
  }
}
