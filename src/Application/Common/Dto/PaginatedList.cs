using Microsoft.EntityFrameworkCore;

namespace Application.Common.Dto;

public class PaginatedList<T>
{
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public List<T> Items { get; }

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageNumber = pageNumber;
        TotalPages = pageSize == 0 ? 0 : (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    /// <summary>
    /// Create the paginated list from query.
    /// </summary>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        int? count, CancellationToken cancellationToken = default)
    {
        count ??= await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count.Value, pageNumber, pageSize);
    }
}