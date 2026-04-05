namespace SpotifyClone.Shared.BuildingBlocks.Application.Pagination;

public class PagedList<T>(IEnumerable<T> items, int count, int page, int pageSize)
{
    public IList<T> Items { get; } = items.ToList();
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = count;
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}
