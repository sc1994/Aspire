namespace Aspire;

public interface ICrudBatch<in TPrimaryKey, TOutput, in TCreate, in TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
{
    Task<IEnumerable<TOutput>> CreateBatchAsync(IEnumerable<TCreate> inputs);

    Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids);

    Task<IEnumerable<TOutput>> UpdateBatchAsync(IEnumerable<TUpdate> inputs);

    Task<IEnumerable<TOutput>> GetListAsync(IEnumerable<TPrimaryKey> ids);
}