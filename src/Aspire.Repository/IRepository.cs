namespace Aspire.Repository;

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    bool IsSoftDeleted { get; }

    Task<IEnumerable<TPrimaryKey>> CreateBatchAsync(IEnumerable<TEntity> inputs);

    Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids);

    Task<int> UpdateBatchAsync(IEnumerable<TEntity> inputs);

    Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> ids);

    Task<TPrimaryKey> CreateAsync(TEntity input);

    Task<bool> DeleteAsync(TPrimaryKey id);

    Task<bool> UpdateAsync(TEntity input);

    Task<TEntity?> GetAsync(TPrimaryKey id);
}