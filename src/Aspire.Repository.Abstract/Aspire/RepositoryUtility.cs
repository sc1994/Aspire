﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspire.Entity;
using Aspire.Entity.Audit;

namespace Aspire
{
    /// <summary>
    ///     仓储实用工具.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public abstract class RepositoryUtility<TEntity, TOrmWhere, TPrimaryKey>
        : IRepository<TEntity, TOrmWhere, TPrimaryKey>
        where TEntity : IEntityBase<TPrimaryKey>
    {
        private readonly ICurrentUser currentUser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryUtility{TEntity, TOrmWhere, TPrimaryKey}" /> class.
        /// </summary>
        /// <param name="currentUser">当前用户.</param>
        protected RepositoryUtility(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        /// <inheritdoc />
        public virtual async Task<TPrimaryKey> CreateAsync(TEntity input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var first = await CreateBatchAsync(input).FirstOrDefaultAsync();
            return first.Id;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetAsync(TPrimaryKey primaryKey)
        {
            if (primaryKey == null) throw new ArgumentNullException(nameof(primaryKey));

            var first = await GetListByAsync(primaryKey).FirstOrDefaultAsync();
            return first;
        }

        /// <inheritdoc />
        public virtual async Task<int> UpdateAsync(TEntity input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

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

            IEnumerable<TEntity> newEntities;
            if (typeof(TEntity).GetInterface(nameof(IAuditCreate)) != null)
                newEntities = entities.Select(x =>
                {
                    var createAudit = x as IAuditCreate;
                    if (createAudit == null)
                        throw new NullReferenceException(
                            $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditCreate)} 产生了 null 结果");

                    createAudit.Creator = currentUser.AccountId;
                    createAudit.CreatedAt = DateTime.Now;
                    return x;
                });
            else
                newEntities = entities;

            return await CreateByEntitiesAsync(newEntities);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetListByAsync(params TPrimaryKey[] primaryKeys)
        {
            return await GetListByPrimaryKeysAsync(primaryKeys);
        }

        /// <inheritdoc />
        public virtual async Task<int> UpdateBatchAsync(params TEntity[] entities)
        {
            if (entities?.Any() != true) return 0;

            IEnumerable<TEntity> newEntities;
            if (typeof(TEntity).GetInterface(nameof(IAuditUpdate)) != null)
                newEntities = entities.Select(x =>
                {
                    var updateAudit = x as IAuditUpdate;
                    if (updateAudit == null)
                        throw new NullReferenceException(
                            $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditUpdate)} 产生了 null 结果");

                    updateAudit.Updater = currentUser.AccountId;
                    updateAudit.UpdatedAt = DateTime.Now;
                    return x;
                });
            else
                newEntities = entities;

            return await UpdateByEntitiesAsync(newEntities);
        }

        /// <inheritdoc />
        public virtual async Task<int> DeleteBatchAsync(params TPrimaryKey[] primaryKeys)
        {
            if (primaryKeys?.Any() != true) return 0;

            if (IsAuditSoftDelete()) return await DeleteByPrimaryKeysAsync(primaryKeys);

            // 做查询删除
            var entities = await GetListByPrimaryKeysAsync(primaryKeys);
            if (entities?.Any() != true) return 0;

            var deleteEntities = entities.Select(x =>
            {
                var deleteAudit = x as IAuditSoftDelete;
                if (deleteAudit == null)
                    throw new NullReferenceException(
                        $"将 {typeof(TEntity)} 显示转换成 {nameof(IAuditUpdate)} 产生了 null 结果");

                deleteAudit.Deleted = true;
                deleteAudit.Deleter = currentUser.AccountId;
                deleteAudit.DeletedAt = DateTime.Now;
                return x;
            });

            return await UpdateByEntitiesAsync(deleteEntities);
        }

        /// <inheritdoc />
        public abstract Task<(long TotalCount, IEnumerable<TEntity> List)> PagingListAsync(
            TOrmWhere where,
            int pageIndex,
            int pageSize);

        /// <summary>
        ///     是否审计软删除.
        /// </summary>
        /// <returns>是否.</returns>
        protected bool IsAuditSoftDelete()
        {
            return typeof(TEntity).GetInterface(nameof(IAuditSoftDelete)) != null;
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