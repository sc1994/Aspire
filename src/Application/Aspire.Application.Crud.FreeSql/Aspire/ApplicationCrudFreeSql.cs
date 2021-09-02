using System.Linq.Expressions;

using Aspire.Entity;

namespace Aspire
{
    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity> : ApplicationCrudFreeSql<
        TEntity,
        Guid>
        where TEntity : class, IEntityBase
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApplicationCrudFreeSql{        TEntity}"/>
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey> : ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TEntity>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApplicationCrudFreeSql{        TEntity,         TPrimaryKey}"/>
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity, TPrimaryKey> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto> : ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TEntity>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApplicationCrudFreeSql{        TEntity,         TPrimaryKey,         TQueryFilterDto}"/>
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity, TPrimaryKey> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputOrInputDto">输出或输入dto.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputOrInputDto> : ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputOrInputDto,
        TOutputOrInputDto>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TOutputOrInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApplicationCrudFreeSql{        TEntity,         TPrimaryKey,         TQueryFilterDto,         TOutputOrInputDto}"/>
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity, TPrimaryKey> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputDto">输出dto.</typeparam>
    /// <typeparam name="TCreateOrUpdateInputDto">创建或更新输入dto.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        Expression<Func<TEntity, bool>>,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TCreateOrUpdateInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="ApplicationCrudFreeSql{        TEntity,         TPrimaryKey,         TQueryFilterDto,         TOutputDto,         TCreateOrUpdateInputDto}" />
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity, TPrimaryKey> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputDto">输出dto.</typeparam>
    /// <typeparam name="TCreateInputDto">创建输入dto.</typeparam>
    /// <typeparam name="TUpdateInputDto">更新输入dto.</typeparam>
    public abstract class ApplicationCrudFreeSql<
        TEntity,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        Expression<Func<TEntity, bool>>,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto>
        where TEntity : class, IEntityBase<TPrimaryKey>
        where TUpdateInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="ApplicationCrudFreeSql{        TEntity,         TPrimaryKey,         TQueryFilterDto,         TOutputDto,         TCreateInputDto,         TUpdateInputDto}" />
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrudFreeSql(IRepositoryFreeSql<TEntity, TPrimaryKey> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }
}