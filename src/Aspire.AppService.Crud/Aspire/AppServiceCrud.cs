using Aspire.Cruds;
using Aspire.Dto;
using Aspire.Entity;

using Microsoft.AspNetCore.Mvc;

namespace Aspire
{
    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere> : AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TEntity>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TOrmWhere}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto> : AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TEntity>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TCommonDto">通用dto.</typeparam>
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TCommonDto> : AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TCommonDto,
        TCommonDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TCommonDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TCommonDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputDto">输出dto.</typeparam>
    /// <typeparam name="TCreateOrUpdateInputDto">创建或者更新输入dto.</typeparam>
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto> : AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto,
        TCreateOrUpdateInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TCreateOrUpdateInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TOutputDto,         TCreateOrUpdateInputDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }

        /// <summary>
        /// 更新或者创建.
        /// </summary>
        /// <param name="input">输入.</param>
        /// <returns>操作是否成功.</returns>
        public virtual async Task<bool> CreateOrUpdateAsync(TCreateOrUpdateInputDto input)
        {
            if (input.Id == null || input.Id.Equals(default(TPrimaryKey)))
            {
                var primaryKey = await CreateAsync(input);
                return primaryKey != null && !primaryKey.Equals(default(TPrimaryKey));
            }
            else
            {
                return (await UpdateAsync(input)) > 0;
            }
        }
    }

    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputDto">输出dto.</typeparam>
    /// <typeparam name="TCreateInputDto">创建输入dto.</typeparam>
    /// <typeparam name="TUpdateInputDto">更新输入dto.</typeparam>
    public abstract class AppServiceCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto> :
        AppServiceBase,
        ICrudSingle<TPrimaryKey, TCreateInputDto, TOutputDto, TUpdateInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TUpdateInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        private readonly IAspireMapper aspireMapper;
        private readonly IRepository<TEntity, TPrimaryKey, TOrmWhere> repository;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="AppServiceCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TOutputDto,         TCreateInputDto,         TUpdateInputDto}" />
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
        {
            this.repository = repository;
            this.aspireMapper = aspireMapper;
        }

        /// <inheritdoc />
        public virtual async Task<TPrimaryKey> CreateAsync(TCreateInputDto input)
        {
            var entity = MapToEntity(input);
            return await repository.CreateAsync(entity);
        }

        /// <inheritdoc />
        public virtual async Task<int> DeleteAsync(TPrimaryKey primaryKey)
        {
            return await repository.DeleteAsync(primaryKey);
        }

        /// <inheritdoc />
        public virtual async Task<TOutputDto> GetAsync(TPrimaryKey primaryKey)
        {
            var entity = await repository.GetAsync(primaryKey);
            return MapToDto(entity);
        }

        /// <inheritdoc />
        public virtual Task<int> UpdateAsync(TUpdateInputDto input)
        {
            var entity = MapToEntity(input);
            return repository.UpdateAsync(entity);
        }

        /// <summary>
        ///     分页查询.
        /// </summary>
        /// <param name="input">查询过滤.</param>
        /// <param name="index">页索引.</param>
        /// <param name="size">页大小.</param>
        /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
        [HttpPost("{index}_{size}")]
        public virtual async Task<PagingOutputDto<TOutputDto>> PagingQueryAsync(
            [FromBody] TQueryFilterDto input,
            [FromQuery] int index = 1,
            [FromQuery] int size = 10)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var (totalCount, list) =
                await repository.PagingListAsync(QueryFilter(input), index, size);

            var dto = MapToDto(list);

            return new PagingOutputDto<TOutputDto>(totalCount, dto);
        }

        /// <summary>
        ///     查询过滤器.
        /// </summary>
        /// <param name="queryParam">过滤参数.</param>
        /// <returns>orm where.</returns>
        protected abstract TOrmWhere QueryFilter(TQueryFilterDto queryParam);

        /// <summary>
        ///     集体映射到dto.
        /// </summary>
        /// <param name="entity">实体.</param>
        /// <returns>dto.</returns>
        protected TOutputDto MapToDto(TEntity entity)
        {
            return aspireMapper.MapTo<TOutputDto>(entity);
        }

        /// <summary>
        ///     集体集合映射到dto集合.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>dto集合.</returns>
        protected IEnumerable<TOutputDto> MapToDto(IEnumerable<TEntity> entities)
        {
            return aspireMapper.MapTo<IEnumerable<TOutputDto>>(entities);
        }

        /// <summary>
        ///     创建dto映射到实体.
        /// </summary>
        /// <param name="createInputDto">创建dto.</param>
        /// <returns>实体.</returns>
        protected TEntity MapToEntity(TCreateInputDto createInputDto)
        {
            return aspireMapper.MapTo<TEntity>(createInputDto ?? throw new ArgumentNullException(nameof(createInputDto)));
        }

        /// <summary>
        ///     更新dto映射到实体.
        /// </summary>
        /// <param name="updateInputDto">更新dto.</param>
        /// <returns>实体.</returns>
        protected TEntity MapToEntity(TUpdateInputDto updateInputDto)
        {
            return aspireMapper.MapTo<TEntity>(updateInputDto);
        }
    }
}