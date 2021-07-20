using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspire.Interfaces
{
    /// <inheritdoc />
    public interface ICrud<TEntity> : ICrud<TEntity, long>
        where TEntity : EntityBase
    {
    }

    /// <inheritdoc />
    public interface ICrud<TEntity, TPrimaryKey> : ICrudSingle<TPrimaryKey, TEntity, TEntity, TEntity>
        where TEntity : EntityBase<TPrimaryKey>
    {
        /// <summary>
        ///     根据实体集合新增数据.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>实体.</returns>
        Task<IEnumerable<TEntity>> CreateBatchAsync(params TEntity[] entities);

        /// <summary>
        ///     根据主键集合获取实体列表.
        /// </summary>
        /// <param name="primaryKeys">主键集合.</param>
        /// <returns>实体.</returns>
        Task<IEnumerable<TEntity>> GetListByAsync(params TPrimaryKey[] primaryKeys);

        /// <summary>
        ///     根据主键和实体示例更新数据.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>受影响行数.</returns>
        Task<int> UpdateBatchAsync(params TEntity[] entities);

        /// <summary>
        ///     根据主键集合删除数据.
        /// </summary>
        /// <param name="primaryKeys">主键集合.</param>
        /// <returns>受影响行数.</returns>
        Task<int> DeleteBatchAsync(params TPrimaryKey[] primaryKeys);
    }
}