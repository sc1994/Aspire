using System.Linq.Expressions;
using Aspire.Repository;
using FreeSql;

namespace Aspire.FreeSql.Extension;

public static class FreeSqlRepositoryExtensions
{
    public static IFreeSql GetFreeSql<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        return repository.GetFreeSqlRepository().FreeSqlInstance;
    }

    public static ISelect<TEntity> Select<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        return repository.GetFreeSqlRepository().Select();
    }

    public static Task<int> DeleteBatchAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, Expression<Func<TEntity, bool>> exp)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        var free = repository.GetFreeSqlRepository();
        return free.DeleteBatchAsync(exp);
    }

    public static Task<int> UpdateBatchAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, Action<IUpdate<TEntity>> updateExp, Expression<Func<TEntity, bool>> exp)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        var free = repository.GetFreeSqlRepository();
        return free.UpdateBatchAsync(updateExp, exp);
    }

    public static Task<IEnumerable<TEntity>> GetListAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, Expression<Func<TEntity, bool>> exp, string orderBy = "", int limit = 0, int skip = 0)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        var free = repository.GetFreeSqlRepository();
        return free.GetListAsync(exp, orderBy, limit, skip);
    }

    public static async Task<(long total, IEnumerable<TEntity> list)> PagingAsync<TEntity, TPrimaryKey>(this ISelect<TEntity> select, int index, int size)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        var task = select.Page(index, size);

        return (await task.CountAsync(), await task.ToListAsync());
    }

    private static FreeSqlRepository<TEntity, TPrimaryKey> GetFreeSqlRepository<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        if (repository is FreeSqlRepository<TEntity, TPrimaryKey> freeSqlRepository)
            return freeSqlRepository;
        throw new Exception("repository is not FreeSqlRepository");
    }
}