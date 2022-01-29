namespace Aspire.Core.Domain;

public static class PageOutDto
{
    public static PageOutDto<TDto> Create<TDto>(long total, IEnumerable<TDto> list)
    {
        return new PageOutDto<TDto>(total, list);
    }
}

public record PageOutDto<TDto>(long TotalCount, IEnumerable<TDto> Items);