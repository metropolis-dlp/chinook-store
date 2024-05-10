namespace ChinookStore.Application._Common.Model;

public class PaginationQuery
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public PaginationQuery(int pageIndex, int pageSize)
    {
      PageIndex = pageIndex;
      PageSize = pageSize;
    }

    public PaginationQuery()
    {
    }
}
