using Aspire.Entity;
using FreeSql;

namespace Aspire
{
    /// <summary>
    ///     仓储.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    public interface IRepository<TEntity, TPrimaryKey>
        : IRepository<TEntity, ISelect<TEntity>, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey>
    {
    }
}