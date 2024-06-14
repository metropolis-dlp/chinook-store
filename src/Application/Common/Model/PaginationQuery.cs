namespace ChinookStore.Application.Common.Model;

public class PaginationQuery
{
    public int Offset { get; init; } = 0;
    public int Size { get; init; } = 10;
}
