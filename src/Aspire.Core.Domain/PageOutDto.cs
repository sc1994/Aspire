namespace Aspire.Core.Domain;

public class PageOutDto<TDto>
{
    public PageOutDto() : this(0, Array.Empty<TDto>())
    {
    }

    public PageOutDto(long totalCount, IEnumerable<TDto> items)
    {
        TotalCount = totalCount;
        Items = items;
    }

    public long TotalCount { get; set; }

    public IEnumerable<TDto> Items { get; set; }
}