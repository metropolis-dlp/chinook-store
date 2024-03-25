namespace ChinookStore.Application._Common.Model;

public class PaginationQuery
{
    public int PageIndex { get; } = 1;
    public int PageSize { get; } = 10;

    public PaginationQuery(int pageIndex, int pageSize)
    {
      PageIndex = pageIndex;
      PageSize = pageSize;
    }

    public PaginationQuery()
    {
    }
}
