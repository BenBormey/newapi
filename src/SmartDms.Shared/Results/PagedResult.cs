namespace JuJuBis.Shared.Results;

/// <summary>
/// Pagination ស្តង់ដារ - គ្រប់ list endpoint គួរប្រើនេះ (professional!)
/// Response: { items, page, pageSize, totalCount, totalPages }
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public long TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;

    public PagedResult(IReadOnlyList<T> items, int page, int pageSize, long totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
