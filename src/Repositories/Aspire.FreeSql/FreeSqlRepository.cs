using System.Linq.Expressions;
using Aspire.Entity;
using Aspire.FreeSql.Entity;
using Aspire.Repository;
using Autofac;
using FreeSql;

namespace Aspire.FreeSql;

public class FreeSqlRepository<TEntity, TPrimaryKey> : BaseRepository<TEntity, TPrimaryKey>
    where TEntity : class, IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    public readonly IFreeSql FreeSqlInstance;

    public FreeSqlRepository(IComponentContext iocContext) : base(iocContext)
    {
        var dbs = iocContext.Resolve<IEnumerable<IFreeSql>>();
        var type = typeof(TEntity);
        var dbName = typeof(IDatabase<>).Name;
        var dbConfig = type.GetInterface(dbName)?.GetGenericArguments().FirstOrDefault();

        if (dbConfig is null)
            FreeSqlInstance = dbs.FirstOrDefault(x => x.GetType().GetGenericArguments().FirstOrDefault() == typeof(IFreeSql)) ?? throw new Exception("没有找到默认数据库"); // TODO 
        else
            FreeSqlInstance = dbs.FirstOrDefault(x => x.GetType().GetGenericArguments().FirstOrDefault() == dbConfig) ?? throw new Exception($"没有找到{dbConfig.Name}数据库"); // TODO
    }

    public ISelect<TEntity> Select()
    {
        var select = FreeSqlInstance.Select<TEntity>();

        if (IsSoftDeleted) select.Where(x => ((IDeleted) x).IsDeleted == false);

        return select;
    }

    public async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> exp)
    {
        if (IsSoftDeleted) return await FreeSqlInstance.Delete<TEntity>().Where(exp).ExecuteAffrowsAsync();

        var res = await UpdateBatchAsync(updater =>
        {
            updater.Set(x => ((IDeleted) x).DeletedAt, DateTime.Now);
            updater.Set(x => ((IDeleted) x).DeletedBy, CurrentUser.UserName);
            updater.Set(x => ((IDeleted) x).IsDeleted, true);
        }, exp);
        return res;
    }

    public async Task<int> UpdateBatchAsync(Action<IUpdate<TEntity>> updateExp, Expression<Func<TEntity, bool>> exp)
    {
        var updater = FreeSqlInstance.Update<TEntity>();
        updateExp(updater);

        return await updater.Where(exp).ExecuteAffrowsAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp, string orderBy = "", int limit = 0, int skip = 0)
    {
        var sql = Select().Where(exp);

        if (!string.IsNullOrWhiteSpace(orderBy)) sql.OrderBy(orderBy);

        if (limit > 0) sql.Take(limit);

        if (skip > 0) sql.Skip(skip);

        return await sql.ToListAsync();
    }

    public override async Task<IEnumerable<TPrimaryKey>> CreateBatchAsync(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return Array.Empty<TPrimaryKey>();

        TrySetCreated(inputs);

        _ = await FreeSqlInstance.Insert(inputs).ExecuteAffrowsAsync();

        return inputs.Select(x => x.Id);
    }

    public override async Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (ids.Any() != true) return 0;

        return await DeleteBatchAsync(x => ids.Contains(x.Id));
    }

    public override async Task<int> UpdateBatchAsync(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return 0;

        TrySetUpdated(inputs);

        return await FreeSqlInstance.Update<TEntity>().SetSource(inputs).ExecuteAffrowsAsync();
    }

    public override async Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (ids.Any() != true) return new List<TEntity>();

        return await GetListAsync(x => ids.Contains(x.Id));
    }
}