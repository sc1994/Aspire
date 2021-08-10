using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspire.Entity;
using FreeSql;

namespace Aspire.Repository.FreeSql
{
    /// <summary>
    ///     free sql 仓储实现.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TDatabase">使用的数据库(针对free sql的多数据形式).</typeparam>
    public class FreeSqlRepository<TEntity, TPrimaryKey, TDatabase>
        : RepositoryUtility<TEntity, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey>
    {
        private readonly IFreeSql<TDatabase> freeSql;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FreeSqlRepository{TEntity, TPrimaryKey, TDatabase}" /> class.
        /// </summary>
        /// <param name="freeSql">free sql.</param>
        /// <param name="currentAccount">当前用户.</param>
        public FreeSqlRepository(IFreeSql<TDatabase> freeSql, ICurrentAccount currentAccount)
            : base(currentAccount)
        {
            this.freeSql = freeSql;
        }

        /// <inheritdoc />
        public override async Task<(long TotalCount, IEnumerable<TEntity> List)> PagingListAsync(
            ISelect<TEntity> where, int pageIndex, int pageSize)
        {
            var listAsync = where
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalAsync = where.CountAsync();

            return (await totalAsync, await listAsync);
        }

        /// <inheritdoc />
        protected override async Task<IEnumerable<TEntity>> CreateByEntitiesAsync(IEnumerable<TEntity> entities)
        {
            return await freeSql.Insert(entities).ExecuteInsertedAsync();
        }

        /// <inheritdoc />
        protected override async Task<int> UpdateByEntitiesAsync(IEnumerable<TEntity> entities)
        {
            return await freeSql.Update<TEntity>().SetSource(entities).ExecuteAffrowsAsync();
        }

        /// <inheritdoc />
        protected override async Task<int> DeleteByPrimaryKeysAsync(IEnumerable<TPrimaryKey> primaryKeys)
        {
            return await freeSql.Delete<TEntity>().Where(x => primaryKeys.Contains(x.Id)).ExecuteAffrowsAsync();
        }

        /// <inheritdoc />
        protected override async Task<IEnumerable<TEntity>> GetListByPrimaryKeysAsync(
            IEnumerable<TPrimaryKey> primaryKeys)
        {
            return await Select().Where(x => primaryKeys.Contains(x.Id)).ToListAsync();
        }

        private ISelect<TEntity> Select()
        {
            if (IsAuditSoftDelete()) return freeSql.Select<TEntity>();

            return freeSql.Select<TEntity>().Where("Deleted = 0");
        }
    }
}