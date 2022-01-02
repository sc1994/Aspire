namespace Aspire.Core.Domain;

public interface ICore<TDto, in TPrimaryKey, in TPageParam>
    where TDto : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    Task<IEnumerable<TDto>> CreateBatchAsync(IEnumerable<TDto> inputs);

    Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids);

    Task<IEnumerable<TDto>> UpdateBatchAsync(IEnumerable<TDto> inputs);

    Task<IEnumerable<TDto>> GetListAsync(IEnumerable<TPrimaryKey> ids);

    Task<PageOutDto<TDto>> PagingAsync(int index, int size, TPageParam input);

    public virtual async Task<TDto> CreateAsync(TDto input)
    {
        var ids = await CreateBatchAsync(new[] {input});
        return ids.FirstOrDefault() ?? throw new Exception("Create failed");
    }

    public virtual async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await DeleteBatchAsync(new[] {id}) == 1;
    }

    public virtual async Task<TDto> UpdateAsync(TDto input)
    {
        return (await UpdateBatchAsync(new[] {input})).FirstOrDefault() ?? throw new Exception("Update failed");
    }

    public virtual async Task<TDto?> GetAsync(TPrimaryKey id)
    {
        return (await GetListAsync(new[] {id})).FirstOrDefault();
    }
}