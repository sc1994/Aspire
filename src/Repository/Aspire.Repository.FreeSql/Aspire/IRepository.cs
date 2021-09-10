using System.Linq.Expressions;

using Aspire.Entity;

using FreeSql;

namespace Aspire
{
    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    public interface IRepository<TEntity>
        : IRepository<TEntity, Guid>
        where TEntity : class, IEntityBase
    {
    }

    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    public interface IRepository<TEntity, TPrimaryKey>
        : IRepositoryBase<TEntity, TPrimaryKey, ISelect<TEntity>>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// free sql 的 select(如果包含软删除, 则会自动过滤).
        /// </summary>
        /// <returns>free sql 的查询过滤接口.</returns>
        ISelect<TEntity> Select();

        /// <summary>
        ///     分页列表.
        /// </summary>
        /// <param name="where">where.</param>
        /// <param name="pageIndex">page index.</param>
        /// <param name="pageSize">page size.</param>
        /// <returns>total count , list.</returns>
        Task<(long TotalCount, IEnumerable<TEntity> List)> PagingListAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize);

        /// <summary>
        ///     根据过滤条件获取列表.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>列表.</returns>
        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where)
            => GetAsync(Select().Where(where));

        /// <summary>
        ///     根据过滤条件获取列表.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>列表.</returns>
        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where)
            => GetListAsync(Select().Where(where));

        /// <summary>
        ///     根据过滤条件获取数据条数.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>数据条数.</returns>
        public Task<long> CountAsync(Expression<Func<TEntity, bool>> where)
            => CountAsync(Select().Where(where));

        /// <summary>
        ///     根据过滤条件获取数据是否存在.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>是否存在.</returns>
        public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> where)
            => ExistAsync(Select().Where(where));
    }

    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    /// <typeparam name="TDatabase">指定当前操作的数据库.</typeparam>
    public interface IRepository<TEntity, TPrimaryKey, TDatabase>
        : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey, TDatabase>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}