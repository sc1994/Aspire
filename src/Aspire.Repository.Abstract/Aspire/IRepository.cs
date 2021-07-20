using System.Collections.Generic;
using System.Threading.Tasks;
using Aspire.Interfaces;

namespace Aspire
{
    /// <inheritdoc />
    public interface IRepository<TEntity, in TOrmWhere> : IRepository<TEntity, TOrmWhere, long>
        where TEntity : EntityBase
    {
    }

    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TOrmWhere">比如ef的IQueryable, freeSql的ISelect.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    public interface IRepository<TEntity, in TOrmWhere, TPrimaryKey> : ICrud<TEntity, TPrimaryKey>
        where TEntity : EntityBase<TPrimaryKey>
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
    }
}