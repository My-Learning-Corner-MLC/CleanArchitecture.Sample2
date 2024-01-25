namespace Sample2.Application.Common.Models;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int? TotalPages { get; }
    public int? TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<T> items, int? count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = count.HasValue ? (int)Math.Ceiling(count.Value / (double)pageSize) : null;
        TotalCount = count.HasValue ? count.Value : null;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool? HasNextPage => TotalPages.HasValue ? PageNumber < TotalPages : null;
}
