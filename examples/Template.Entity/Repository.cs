using System.Linq.Expressions;
using Autofac;
using FreeSql;
using FreeSql.DataAnnotations;
using Template.Util;

// ReSharper disable PossibleMultipleEnumeration

namespace Template.Entity;

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

    private static FreeSqlRepository<TEntity, TPrimaryKey> GetFreeSqlRepository<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        if (repository is FreeSqlRepository<TEntity, TPrimaryKey> freeSqlRepository)
            return freeSqlRepository;
        throw new Exception("repository is not FreeSqlRepository");
    }
}

public class FreeSqlRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey>, IFreeSqlExtensions<TEntity, TPrimaryKey>
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

    public async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> exp)
    {
        if (!IsDeleted()) return await FreeSqlInstance.Delete<TEntity>().Where(exp).ExecuteAffrowsAsync();

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
        var sql = FreeSqlInstance.Select<TEntity>().Where(exp);

        if (!string.IsNullOrWhiteSpace(orderBy)) sql.OrderBy(orderBy);

        if (limit > 0) sql.Take(limit);

        if (skip > 0) sql.Skip(skip);

        return await sql.ToListAsync();
    }

    public ISelect<TEntity> Select()
    {
        var select = FreeSqlInstance.Select<TEntity>();

        if (IsDeleted()) select.Where(x => ((IDeleted) x).IsDeleted == false);

        return select;
    }

    public override async Task<IEnumerable<TEntity>> CreateBatchAsync(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return Array.Empty<TEntity>();

        TrySetCreated(inputs);

        return await FreeSqlInstance.Insert(inputs).ExecuteInsertedAsync();
    }

    public override async Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (ids.Any() != true) return 0;

        return await DeleteBatchAsync(x => ids.Contains(x.Id));
    }

    public override async Task<IEnumerable<TEntity>> UpdateBatchAsync(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return new List<TEntity>();

        TrySetUpdated(inputs);
        var rows = await FreeSqlInstance.Update<TEntity>().SetSource(inputs).ExecuteAffrowsAsync();
        if (rows != inputs.Count()) throw new Exception("更新失败");

        return await GetListAsync(inputs.Select(x => x.Id));
    }

    public override async Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (ids.Any() != true) return new List<TEntity>();

        return await GetListAsync(x => ids.Contains(x.Id));
    }
}

public interface IFreeSqlExtensions<TEntity, TPrimaryKey>
    where TEntity : class, IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> exp);

    Task<int> UpdateBatchAsync(Action<IUpdate<TEntity>> updateExp, Expression<Func<TEntity, bool>> exp);

    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp, string orderBy = "", int limit = 0, int skip = 0);
}

public abstract class FullEntity<TPrimaryKey> : IPrimaryKey<TPrimaryKey>, ICreated, IUpdated, IDeleted
    where TPrimaryKey : struct, IEquatable<TPrimaryKey>
{
    [Column(CanUpdate = false)] public DateTime CreatedAt { get; set; } = Const.DefaultDateTime;

    [Column(CanUpdate = false)] public string CreatedBy { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public DateTime DeletedAt { get; set; } = Const.DefaultDateTime;

    public string DeletedBy { get; set; } = string.Empty;

    [Column(IsIdentity = true, IsPrimary = true, CanUpdate = false)]
    public TPrimaryKey Id { get; set; }

    public DateTime UpdatedAt { get; set; } = Const.DefaultDateTime;

    public string UpdatedBy { get; set; } = string.Empty;
}

// ReSharper disable once UnusedTypeParameter
public interface IDatabase<TDatabase>
{
}

public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    protected readonly ICurrentUser CurrentUser;

    protected Repository(IComponentContext iocContext)
    {
        IocContext = iocContext;
        CurrentUser = iocContext.Resolve<ICurrentUser>();
    }

    public IComponentContext IocContext { get; }

    public abstract Task<IEnumerable<TEntity>> CreateBatchAsync(IEnumerable<TEntity> inputs);

    public abstract Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids);

    public abstract Task<IEnumerable<TEntity>> UpdateBatchAsync(IEnumerable<TEntity> inputs);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> ids);

    public async Task<TEntity> CreateAsync(TEntity input)
    {
        var res = await CreateBatchAsync(new[] {input});

        return res.FirstOrDefault() ?? throw new Exception("创建失败");
    }

    public async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        var res = await DeleteBatchAsync(new[] {id});

        return res == 1;
    }

    public async Task<TEntity> UpdateAsync(TEntity input)
    {
        var res = await UpdateBatchAsync(new[] {input});

        return res.FirstOrDefault() ?? throw new Exception("更新失败");
    }

    public async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        var res = await GetListAsync(new[] {id});

        return res.FirstOrDefault() ?? throw new Exception("查询失败");
    }

    public void TrySetCreated(IEnumerable<TEntity> inputs)
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

    public void TrySetUpdated(IEnumerable<TEntity> inputs)
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

    public bool IsDeleted()
    {
        return typeof(TEntity).GetInterface(nameof(IDeleted)) is not null;
    }

    public void TrySetDeleted(IEnumerable<TEntity> inputs)
    {
        if (inputs.Any() != true) return;
        if (!IsDeleted()) return;

        foreach (var input in inputs)
        {
            var deleted = (IDeleted) input;

            deleted.IsDeleted = true;
            deleted.DeletedAt = DateTime.Now;
            deleted.DeletedBy = CurrentUser.UserName;
        }
    }
}

public interface IRepository<TEntity, in TPrimaryKey> : ICrudBatch<TPrimaryKey, TEntity, TEntity, TEntity>, ICrud<TPrimaryKey, TEntity, TEntity, TEntity>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    IComponentContext IocContext { get; }

    void TrySetCreated(IEnumerable<TEntity> inputs);

    void TrySetUpdated(IEnumerable<TEntity> inputs);

    bool IsDeleted();

    void TrySetDeleted(IEnumerable<TEntity> inputs);
}