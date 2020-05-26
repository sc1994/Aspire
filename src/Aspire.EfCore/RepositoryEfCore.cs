using Aspire.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspire.Domain.Repositories
{
    public class RepositoryEfCore<TEntity> : RepositoryEfCore<TEntity, long>, IRepositoryEfCore<TEntity>
        where TEntity : BaseEntity
    {
        public RepositoryEfCore(DbContext dbContext) : base(dbContext) { }
    }

    public class RepositoryEfCore<TEntity, TId> : Repository<TEntity, TId>, IRepositoryEfCore<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly DbContext _dbContext;
        public RepositoryEfCore(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string _tableName = null;
        public override string TableName
            => GetOrSet(ref _tableName, () => EntityType.GetTableName());

        private string[] _primaryKeyNames = null;
        public override string[] PrimaryKeyNames
            => GetOrSet(ref _primaryKeyNames, () => EntityType.FindPrimaryKey().Properties.Select(x => x.Name).ToArray());

        [Obsolete("暂未实现", true)]
        public override string[] IdentityNames => throw new NotImplementedException();

        private static DbSet<TEntity> _query;
        public DbSet<TEntity> Query
            => GetOrSet(ref _query, () => _dbContext.Set<TEntity>());

        private IEntityType _entityType = default;
        public IEntityType EntityType
            => GetOrSet(ref _entityType, () => _dbContext.Model.FindEntityType(typeof(TEntity)));

        private T GetOrSet<T>(ref T value, Func<T> geter)
        {
            if (value != null) return value;
            return value = geter();
        }

        public override async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public override async Task<TId[]> AddRangeThenIdsAsync(IEnumerable<TEntity> entities)
        {
            var ls = new List<TEntity>();
            foreach (TEntity entity in entities)
            {
                var t = await _dbContext.AddAsync(entity);
                ls.Add(t.Entity);
            }
            _ = await _dbContext.SaveChangesAsync();
            return ls.Select(x => x.Id).ToArray();
        }

        public override async Task<TEntity[]> AddRangeThenEntitiesAsync(IEnumerable<TEntity> entities)
        {
            var ls = new List<TEntity>();
            foreach (TEntity entity in entities)
            {
                var t = await _dbContext.AddAsync(entity);
                ls.Add(t.Entity);
            }
            _ = await _dbContext.SaveChangesAsync();
            return ls.ToArray();
        }

        public override async Task<int> ExecuteSqlRawAsync(string sql, params object[] param)
            => await _dbContext.Database.ExecuteSqlRawAsync(sql, param);

        public override async Task<TEntity[]> QuerySqlRawAsync(string sql, params object[] param)
            => await Query.FromSqlRaw(sql, param).ToArrayAsync();

        public override async Task<TEntity[]> GetByIdsAsync(IEnumerable<TId> ids)
            => await Query.Where(x => ids.Contains(x.Id)).ToArrayAsync();

        public override async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            entities = entities.Select(x => { x.UpdatedAt = DateTime.Now; return x; });
            return await UpdatePurityAsync(entities);
        }

        public virtual async Task<bool> UpdateAsync(Action<TEntity> updater, TId id)
            => await UpdateRangeAsync(updater, id) == 1;

        public virtual async Task<int> UpdateRangeAsync(Action<TEntity> updater, params TId[] ids)
            => await UpdateRangeAsync(updater, ids.AsEnumerable());

        public virtual async Task<int> UpdateRangeAsync(Action<TEntity> updater, IEnumerable<TId> ids)
        {
            var ls = await GetByIdsAsync(ids);
            Query.AttachRange(ls);
            ls.ToList().ForEach(x => { updater(x); x.UpdatedAt = DateTime.Now; });
            return await UpdatePurityAsync(ls);
        }

        private async Task<int> UpdatePurityAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
