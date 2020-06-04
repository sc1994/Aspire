using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aspire.Domain.Entities;

namespace Aspire.Domain.Repositories
{
    public abstract class Repository<TEntity> : Repository<TEntity, long>, IRepository<TEntity>
        where TEntity : BaseEntity
    {

    }

    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        public virtual async Task<bool> AddAsync(TEntity entity)
             => await AddRangeAsync(entity) == 1;
        public virtual async Task<TId> AddThenIdAsync(TEntity entity)
            => (await AddRangeThenIdsAsync(entity)).First();
        public virtual async Task<int> AddRangeAsync(params TEntity[] entities)
            => await AddRangeAsync(entities.AsEnumerable());
        public virtual async Task<TEntity> AddThenEntityAsync(TEntity entity)
            => (await AddRangeThenEntitiesAsync(entity)).First();
        public virtual async Task<TEntity[]> AddRangeThenEntitiesAsync(params TEntity[] entities)
            => await AddRangeThenEntitiesAsync(entities.AsEnumerable());
        public virtual async Task<TId[]> AddRangeThenIdsAsync(params TEntity[] entities)
            => await AddRangeThenIdsAsync(entities.AsEnumerable());
        public virtual async Task<bool> DeleteAsync(TId id)
            => await DeleteRangeAsync(id) == 1;
        public virtual async Task<int> DeleteRangeAsync(params TId[] ids)
            => await DeleteRangeAsync(ids.AsEnumerable());
        public virtual async Task<bool> DeleteAsync(TEntity entity)
            => await DeleteRangeAsync(entity) == 1;
        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TId> ids)
            => await DeleteRangeAsync(await GetByIdsAsync(ids));
        public virtual async Task<int> DeleteRangeAsync(params TEntity[] entities)
            => await DeleteRangeAsync(entities.AsEnumerable());
        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
            => await UpdateRangeAsync(entities.Select(SoftDelete)); // 软删除
        private TEntity SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
            => await UpdateRangeAsync(entity) == 1;
        public virtual async Task<int> UpdateRangeAsync(params TEntity[] entities)
            => await UpdateRangeAsync(entities.AsEnumerable());
        public virtual async Task<TEntity[]> UpdateRangeThenEntitiesAsync(params TEntity[] entities)
            => await UpdateRangeThenEntitiesAsync(entities.AsEnumerable());
        public virtual async Task<TEntity> UpdateThenEntityAsync(TEntity entity)
        {
            if (await UpdateAsync(entity))
                return entity;

            throw new System.Exception("异常详情待完善(更新数据失败)");
        }
        public virtual async Task<TEntity[]> UpdateRangeThenEntitiesAsync(IEnumerable<TEntity> entities)
        {
            if (await UpdateRangeAsync(entities) == entities.Count())
                return await GetByIdsAsync(entities.Select(x => x.Id));

            throw new System.Exception("异常详情待完善(更新数据失败)");
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
            => (await GetByIdsAsync(id)).First();
        public virtual async Task<TEntity[]> GetByIdsAsync(params TId[] ids)
            => await GetByIdsAsync(ids.AsEnumerable());


        public abstract string TableName { get; }
        public abstract string[] PrimaryKeyNames { get; }
        public abstract string[] IdentityNames { get; }


        public abstract Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        public abstract Task<TId[]> AddRangeThenIdsAsync(IEnumerable<TEntity> entities);

        public abstract Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        public abstract Task<TEntity[]> GetByIdsAsync(IEnumerable<TId> ids);

        public abstract Task<int> ExecuteSqlRawAsync(string sql, params object[] param);
        public abstract Task<TEntity[]> QuerySqlRawAsync(string sql, params object[] param);
        public abstract Task<TEntity[]> AddRangeThenEntitiesAsync(IEnumerable<TEntity> entities);
    }
}
