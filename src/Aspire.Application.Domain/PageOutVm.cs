namespace Aspire.Application.Domain;

public class PageOutVm<T>
{
    public PageOutVm() : this(0, Array.Empty<T>())
    {
    }

    public PageOutVm(long totalCount, IEnumerable<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }

    public long TotalCount { get; set; }

    public IEnumerable<T> Items { get; set; }
}