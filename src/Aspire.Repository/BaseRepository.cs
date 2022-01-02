using Aspire.Entity;
using Autofac;

namespace Aspire.Repository;

public abstract class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    protected BaseRepository(IComponentContext iocContext)
    {
        CurrentUser = iocContext.Resolve<ICurrentUser>();
    }

    protected ICurrentUser CurrentUser { get; }
    public virtual bool IsSoftDeleted => typeof(TEntity).GetInterface(nameof(IDeleted)) is not null;

    public abstract Task<IEnumerable<TPrimaryKey>> CreateBatchAsync(IEnumerable<TEntity> inputs);

    public abstract Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids);

    public abstract Task<int> UpdateBatchAsync(IEnumerable<TEntity> inputs);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> ids);

    public virtual async Task<TPrimaryKey> CreateAsync(TEntity input)
    {
        var ids = await CreateBatchAsync(new[] {input});
        return ids.FirstOrDefault() ?? throw new Exception("Create failed");
    }

    public virtual async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await DeleteBatchAsync(new[] {id}) == 1;
    }

    public virtual async Task<bool> UpdateAsync(TEntity input)
    {
        return await UpdateBatchAsync(new[] {input}) == 1;
    }

    public virtual async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        return (await GetListAsync(new[] {id})).FirstOrDefault();
    }

    protected virtual void TrySetCreated(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return;
        if (typeof(TEntity).GetInterface(nameof(ICreated)) is null) return;

        foreach (var input in inputs)
        {
            var created = (ICreated) input;

            created.CreatedAt = DateTime.Now;
            created.CreatedBy = CurrentUser.UserName;
        }
    }

    protected virtual void TrySetUpdated(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return;
        if (typeof(TEntity).GetInterface(nameof(IUpdated)) is null) return;

        foreach (var input in inputs)
        {
            var updated = (IUpdated) input;

            updated.UpdatedAt = DateTime.Now;
            updated.UpdatedBy = CurrentUser.UserName;
        }
    }

    protected virtual void TrySetDeleted(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return;
        if (!IsSoftDeleted) return;

        foreach (var input in inputs)
        {
            var deleted = (IDeleted) input;

            deleted.IsDeleted = true;
            deleted.DeletedAt = DateTime.Now;
            deleted.DeletedBy = CurrentUser.UserName;
        }
    }
}