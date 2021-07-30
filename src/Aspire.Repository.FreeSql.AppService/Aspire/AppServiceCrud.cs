using Aspire.Entity;
using FreeSql;

namespace Aspire
{
    /// <inheritdoc />
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto> :
        AppServiceCrud<
            TEntity,
            ISelect<TEntity>,
            TPrimaryKey,
            TQueryFilterDto,
            TOutputDto,
            TCreateInputDto,
            TUpdateInputDto>
        where TEntity : class, IEntityBase<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TQueryFilterDto,         TOutputDto,         TCreateInputDto,         TUpdateInputDto}"/> class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="aspireMapper"></param>
        protected AppServiceCrud(
            IRepository<TEntity, TPrimaryKey> repository,
            IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }
}