using Aspire.Entity;
using FreeSql;

namespace Aspire
{
    /// <inheritdoc />
    public abstract class RepositoryUtility<TEntity, TPrimaryKey>
        : RepositoryUtility<TEntity, ISelect<TEntity>, TPrimaryKey>
        where TEntity : class, IEntityBase<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryUtility{TEntity, TPrimaryKey}" /> class.
        /// </summary>
        /// <param name="account">当前用户.</param>
        protected RepositoryUtility(IAccount account)
            : base(account)
        {
        }
    }
}