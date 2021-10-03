using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aspire.Entity;
using Aspire.Helpers;
using FreeSql;

namespace Aspire.Repository.FreeSql
{
    /// <summary>
    ///     free sql 仓储实现.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    public class RepositoryFreeSql<TEntity> : RepositoryFreeSql<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntityBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryFreeSql{TEntity}" /> class.
        /// </summary>
        /// <param name="freeSql">free sql.</param>
        public RepositoryFreeSql(IFreeSql freeSql)
            : base(freeSql)
        {
        }
    }

    /// <summary>
    ///     free sql 仓储实现.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public class RepositoryFreeSql<TEntity, TPrimaryKey>
          : RepositoryUtility<TEntity, TPrimaryKey, ISelect<TEntity>>, IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        private readonly IFreeSql freeSql;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryFreeSql{TEntity, TPrimaryKey}" /> class.
        /// </summary>
        /// <param name="freeSql">free sql.</param>
        public RepositoryFreeSql(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
        }

        /// <inheritdoc />
        public override async Task<long> CountAsync(ISelect<TEntity> where)
        {
            return await where.CountAsync();
        }

        /// <inheritdoc />
        public override async Task<bool> ExistAsync(ISelect<TEntity> where)
        {
            return await CountAsync(where) > 0;
        }

        /// <inheritdoc />
        public override async Task<bool> ExistAsync(TPrimaryKey primaryKey)
        {
            return await Select().Where(x => x.Id.Equals(primaryKey)).CountAsync() == 1;
        }

        /// <inheritdoc />
        public override async Task<TEntity?> GetAsync(ISelect<TEntity> where)
        {
            return await where.FirstAsync();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<TEntity>> GetListAsync(ISelect<TEntity> where)
        {
            return await where.ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<(long totalCount, IEnumerable<TEntity> list)> PagingListAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize)
        {
            return await PagingListAsync(Select().Where(where), pageIndex, pageSize);
        }

        /// <inheritdoc />
        public override async Task<(long totalCount, IEnumerable<TEntity> list)> PagingListAsync(ISelect<TEntity> where, int pageIndex, int pageSize)
        {
            var w = where;
            var listAsync = w
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalAsync = w.CountAsync();

            return (await totalAsync, await listAsync);
        }

        /// <inheritdoc />
        public ISelect<TEntity> Select()
        {
            if (IsAuditSoftDelete()) return freeSql.Select<TEntity>();

            return freeSql.Select<TEntity>().Where("Deleted = 0").OrderByDescending(x => x.Id);
        }

        /// <inheritdoc />
        protected override async Task<IEnumerable<TEntity>> CreateByEntitiesAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            entities.ForEach(x => x.Id = default);
#pragma warning restore CS8601 // Possible null reference assignment.

            return await freeSql.Insert(entities).ExecuteInsertedAsync();
        }

        /// <inheritdoc />
        protected override async Task<int> UpdateByEntitiesAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return await freeSql.Update<TEntity>().SetSource(entities).ExecuteAffrowsAsync();
        }

        /// <inheritdoc />
        protected override async Task<int> DeleteByPrimaryKeysAsync(IEnumerable<TPrimaryKey> primaryKeys)
        {
            if (primaryKeys is null)
            {
                throw new ArgumentNullException(nameof(primaryKeys));
            }

            return await freeSql.Delete<TEntity>().Where(x => primaryKeys.Contains(x.Id)).ExecuteAffrowsAsync();
        }

        /// <inheritdoc />
        protected override async Task<IEnumerable<TEntity>> GetListByPrimaryKeysAsync(IEnumerable<TPrimaryKey> primaryKeys)
        {
            if (primaryKeys is null)
            {
                throw new ArgumentNullException(nameof(primaryKeys));
            }

            return await Select().Where(x => primaryKeys.Contains(x.Id)).ToListAsync();
        }
    }

    /// <summary>
    ///     free sql 仓储实现.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TDatabase">使用的数据库(针对free sql的多数据形式).</typeparam>
    public class RepositoryFreeSql<TEntity, TPrimaryKey, TDatabase>
        : RepositoryFreeSql<TEntity, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey, TDatabase>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryFreeSql{TEntity, TPrimaryKey, TDatabase}"/> class.
        /// </summary>
        /// <param name="freeSql">free sql.</param>
        public RepositoryFreeSql(IFreeSql freeSql)
            : base(freeSql)
        {
        }
    }
}