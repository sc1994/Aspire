using System.Collections.Generic;
using System.Threading.Tasks;

using Aspire.Domain.Entities;

namespace Aspire.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, long>
       where TEntity : BaseEntity
    {

    }

    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        #region 增
        /// <summary> 增 </summary>
        Task<bool> AddAsync(TEntity entity);
        /// <summary> 增 而后取得Id </summary>
        Task<TId> AddThenIdAsync(TEntity entity);
        /// <summary> 增 而后取得实体 </summary>
        Task<TEntity> AddThenEntityAsync(TEntity entity);
        /// <summary> 增 批量 </summary>
        Task<int> AddRangeAsync(params TEntity[] entities);
        /// <summary> 增 批量 而后取得Id </summary>
        Task<TId[]> AddRangeThenIdsAsync(params TEntity[] entities);
        /// <summary> 增 批量 而后取得实体 </summary>
        Task<TEntity[]> AddRangeThenEntitiesAsync(params TEntity[] entities);
        /// <summary> 增 批量 </summary>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary> 增 批量 而后取得Id </summary>
        Task<TId[]> AddRangeThenIdsAsync(IEnumerable<TEntity> entities);
        /// <summary> 增 批量 而后取得实体 </summary>
        Task<TEntity[]> AddRangeThenEntitiesAsync(IEnumerable<TEntity> entities);
        #endregion

        #region 删
        /// <summary> 删</summary>
        Task<bool> DeleteAsync(TId id);
        /// <summary> 删 批量</summary>
        Task<int> DeleteRangeAsync(params TId[] ids);
        /// <summary> 删 批量</summary>
        Task<int> DeleteRangeAsync(IEnumerable<TId> ids);
        /// <summary> 删</summary>
        Task<bool> DeleteAsync(TEntity entity);
        /// <summary> 删 批量</summary>
        Task<int> DeleteRangeAsync(params TEntity[] entities);
        /// <summary> 删 批量</summary>
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region 改
        /// <summary> 改 </summary>
        Task<bool> UpdateAsync(TEntity entity);
        /// <summary> 改 </summary>
        Task<TEntity> UpdateThenEntityAsync(TEntity entity);
        /// <summary> 改 批量</summary>
        Task<int> UpdateRangeAsync(params TEntity[] entities);
        /// <summary> 改 批量</summary>
        Task<TEntity[]> UpdateRangeThenEntitiesAsync(params TEntity[] entities);
        /// <summary> 改 批量</summary>
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
        /// <summary> 改 批量</summary>
        Task<TEntity[]> UpdateRangeThenEntitiesAsync(IEnumerable<TEntity> entities);
        #endregion

        #region 查
        /// <summary> 根据主键查询 </summary>
        Task<TEntity> GetByIdAsync(TId id);
        /// <summary> 根据主键查询 </summary>
        Task<TEntity[]> GetByIdsAsync(params TId[] ids);
        /// <summary> 根据主键查询 </summary>
        Task<TEntity[]> GetByIdsAsync(IEnumerable<TId> ids);
        #endregion

        #region 执行SQL
        /// <summary> 执行原始SQL </summary>
        Task<int> ExecuteSqlRawAsync(string sql, params object[] param);

        /// <summary> 查询原始SQL </summary>
        Task<TEntity[]> QuerySqlRawAsync(string sql, params object[] param);
        #endregion

        #region 基础信息
        /// <summary> 表名 </summary>
        string TableName { get; }

        /// <summary> 主键名 </summary>
        string[] PrimaryKeyNames { get; }

        /// <summary> 增量名 </summary>
        string[] IdentityNames { get; }
        #endregion
    }
}
