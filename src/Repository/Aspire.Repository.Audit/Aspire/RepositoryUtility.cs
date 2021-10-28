using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspire.Entity;
using Aspire.Entity.Audit;
using Aspire.Helpers;

namespace Aspire
{
    /// <summary>
    ///     仓储实用工具.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    public abstract class RepositoryUtility<TEntity, TPrimaryKey, TOrmWhere>
        : IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <inheritdoc />
        public virtual async Task<TPrimaryKey?> CreateAsync(TEntity input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var first = await CreateBatchAsync(input).FirstOrDefaultAsync();
            if (first == null)
            {
                return default;
            }

            return first.Id;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> GetAsync(TPrimaryKey primaryKey)
        {
            if (primaryKey == null) throw new ArgumentNullException(nameof(primaryKey));

            var first = await GetListAsync(primaryKey).FirstOrDefaultAsync();
            return first;
        }

        /// <inheritdoc />
        public virtual async Task<int> UpdateAsync(TPrimaryKey primaryKey, TEntity input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            input.Id = primaryKey;
            return await UpdateBatchAsync(input);
        }

        /// <inheritdoc />
        public virtual async Task<int> DeleteAsync(TPrimaryKey primaryKey)
        {
            if (primaryKey == null) throw new ArgumentNullException(nameof(primaryKey));

            return await DeleteBatchAsync(primaryKey);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> CreateBatchAsync(params TEntity[] entities)
        {
            if (entities?.Any() != true) return new List<TEntity>();

            SetAuditCreateValue(ref entities);

            return await CreateByEntitiesAsync(entities);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetListAsync(params TPrimaryKey[] primaryKeys)
        {
            return await GetListByPrimaryKeysAsync(primaryKeys);
        }

        /// <inheritdoc />
        public virtual async Task<int> UpdateBatchAsync(params TEntity[] entities)
        {
            if (entities?.Any() != true) return 0;

            SetAuditUpdateValue(ref entities);

            return await UpdateByEntitiesAsync(entities);
        }

        /// <inheritdoc />
        public virtual async Task<int> DeleteBatchAsync(params TPrimaryKey[] primaryKeys)
        {
            if (primaryKeys?.Any() != true) return 0;

            if (!IsAuditSoftDelete()) return await DeleteByPrimaryKeysAsync(primaryKeys);

            // 做查询删除
            var entities = (await GetListByPrimaryKeysAsync(primaryKeys))?.ToArray();
            if (entities?.Any() != true) return 0;

            SetAuditDeleteValue(ref entities);

            return await UpdateByEntitiesAsync(entities);
        }

        /// <inheritdoc />
        public abstract Task<(long totalCount, IEnumerable<TEntity> list)> PagingListAsync(
            TOrmWhere where,
            int pageIndex,
            int pageSize);

        /// <inheritdoc />
        public abstract Task<TEntity?> GetAsync(TOrmWhere where);

        /// <inheritdoc />
        public abstract Task<IEnumerable<TEntity>> GetListAsync(TOrmWhere where);

        /// <inheritdoc />
        public abstract Task<long> CountAsync(TOrmWhere where);

        /// <inheritdoc />
        public abstract Task<bool> ExistAsync(TOrmWhere where);

        /// <inheritdoc />
        public abstract Task<bool> ExistAsync(TPrimaryKey primaryKey);

        /// <summary>
        ///     是否审计软删除.
        /// </summary>
        /// <returns>是否.</returns>
        protected virtual bool IsAuditSoftDelete()
        {
            return typeof(TEntity).GetInterface(nameof(IAuditSoftDelete)) != null;
        }

        /// <summary>
        /// 设置 create 审计值.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        protected virtual void SetAuditCreateValue(ref TEntity[] entities)
        {
            if (typeof(TEntity).GetInterface(nameof(IAuditCreate)) != null)
                entities.ForEach(x =>
                {
                    var createAudit = x as IAuditCreate;
                    if (createAudit == null)
                        throw new NullReferenceException(
                            $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditCreate)} 产生了 null 结果");

                    createAudit.CreatedAt = DateTime.Now;
                });
        }

        /// <summary>
        /// 设置 update 审计值.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        protected virtual void SetAuditUpdateValue(ref TEntity[] entities)
        {
            if (typeof(TEntity).GetInterface(nameof(IAuditUpdate)) != null)
                entities.ForEach(x =>
                {
                    var updateAudit = x as IAuditUpdate;
                    if (updateAudit == null)
                        throw new NullReferenceException(
                            $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditUpdate)} 产生了 null 结果");

                    updateAudit.UpdatedAt = DateTime.Now;
                });
        }

        /// <summary>
        /// 设置 delete 审计值.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        protected virtual void SetAuditDeleteValue(ref TEntity[] entities)
        {
            entities.ForEach(x =>
            {
                var deleteAudit = x as IAuditSoftDelete;
                if (deleteAudit == null)
                    throw new NullReferenceException(
                        $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditUpdate)} 产生了 null 结果");

                deleteAudit.Deleted = true;
                deleteAudit.DeletedAt = DateTime.Now;
            });
        }

        /// <summary>
        ///     根据实体集合创建数据.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>创建后的实体集合.</returns>
        protected abstract Task<IEnumerable<TEntity>> CreateByEntitiesAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     根据实体集合更新数据.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>受影响行数.</returns>
        protected abstract Task<int> UpdateByEntitiesAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     根据主键集合 物理删除数据. 如不需要可在此处抛出异常.
        /// </summary>
        /// <param name="primaryKeys">主键集合.</param>
        /// <returns>受影响行数.</returns>
        protected abstract Task<int> DeleteByPrimaryKeysAsync(IEnumerable<TPrimaryKey> primaryKeys);

        /// <summary>
        ///     根据主键集合获取列表.
        /// </summary>
        /// <param name="primaryKeys">主键集合.</param>
        /// <returns>列表.</returns>
        protected abstract Task<IEnumerable<TEntity>> GetListByPrimaryKeysAsync(IEnumerable<TPrimaryKey> primaryKeys);
    }
}