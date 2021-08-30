using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Aspire.Cruds;
using Aspire.Entity;

namespace Aspire
{
    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TOrmWhere">比如ef的IQueryable, freeSql的ISelect.</typeparam>
    public interface IRepository<TEntity, TOrmWhere> : IRepository<TEntity, Guid, TOrmWhere>
        where TEntity : IEntityBase
    {
    }

    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    /// <typeparam name="TOrmWhere">比如ef的IQueryable, freeSql的ISelect.</typeparam>
    public interface IRepository<TEntity, TPrimaryKey, TOrmWhere> : ICrud<TEntity, TPrimaryKey>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     分页列表.
        /// </summary>
        /// <param name="where">where.</param>
        /// <param name="pageIndex">page index.</param>
        /// <param name="pageSize">page size.</param>
        /// <returns>total count , list.</returns>
        Task<(long TotalCount, IEnumerable<TEntity> List)>
            PagingListAsync(TOrmWhere where, int pageIndex, int pageSize);

        /// <summary>
        ///     根据过滤条件获取列表.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>列表.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(TOrmWhere where);

        /// <summary>
        ///     根据过滤条件获取数据条数.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>数据条数.</returns>
        Task<long> CountAsync(TOrmWhere where);

        /// <summary>
        ///     根据过滤条件获取数据是否存在.
        /// </summary>
        /// <param name="where">过滤内容.</param>
        /// <returns>是否存在.</returns>
        Task<bool> ExistAsync(TOrmWhere where);

        /// <summary>
        ///     根据主键获取数据是否存在.
        /// </summary>
        /// <param name="primaryKey">主键.</param>
        /// <returns>是否存在.</returns>
        Task<bool> ExistAsync(TPrimaryKey primaryKey);
    }
}