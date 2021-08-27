using Aspire.Entity;
using FreeSql;

namespace Aspire
{
    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    public interface IRepositoryFreeSql<TEntity, TPrimaryKey>
        : IRepository<TEntity, TPrimaryKey, ISelect<TEntity>>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }

    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    /// <typeparam name="TDatabase">指定当前操作的数据库.</typeparam>
    public interface IRepositoryFreeSql<TEntity, TPrimaryKey, TDatabase>
        : IRepository<TEntity, TPrimaryKey, ISelect<TEntity>>
        where TEntity : class, IEntityBase<TPrimaryKey, TDatabase>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}