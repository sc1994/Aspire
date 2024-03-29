using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aspire.Cruds;
using Aspire.Dto;
using Aspire.Entity;
using Aspire.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Aspire
{
    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    public abstract class ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TEntity>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCrud{        TEntity,         TPrimaryKey,         TOrmWhere}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrud(IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
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
    public abstract class ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TEntity>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrud(IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
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
    /// <typeparam name="TOutputOrInputDto">输入或输出dto.</typeparam>
    public abstract class ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputOrInputDto> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputOrInputDto,
        TOutputOrInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TOutputOrInputDto : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TCommonDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrud(IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
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
    public abstract class ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto> : ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateOrUpdateInputDto,
        TCreateOrUpdateInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TOutputDto : IPrimaryKey<TPrimaryKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TOutputDto,         TCreateOrUpdateInputDto}"/> class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrud(IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
            : base(repository, aspireMapper)
        {
        }

        /// <summary>
        /// 更新或者创建.
        /// </summary>
        /// <param name="primaryKey">主键, 可不填.</param>
        /// <param name="input">输入.</param>
        /// <returns>操作是否成功.</returns>
        public virtual async Task<bool> CreateOrUpdateAsync(TPrimaryKey primaryKey, [FromBody] TCreateOrUpdateInputDto input)
        {
            if (primaryKey == null || primaryKey.Equals(default))
            {
                var tmp = await CreateAsync(input);
                return tmp != null && !tmp.Equals(default);
            }
            else
            {
                return (await UpdateAsync(primaryKey, input)) > 0;
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
    public abstract class ApplicationCrud<
        TEntity,
        TPrimaryKey,
        TOrmWhere,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto> :
        ApplicationBase,
        ICrudSingle<TPrimaryKey, TCreateInputDto, TOutputDto, TUpdateInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TOutputDto : IPrimaryKey<TPrimaryKey>
    {
        private readonly IAspireMapper aspireMapper;
        private readonly IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="ApplicationCrud{        TEntity,         TPrimaryKey,         TOrmWhere,         TQueryFilterDto,         TOutputDto,         TCreateInputDto,         TUpdateInputDto}" />
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected ApplicationCrud(IRepositoryBase<TEntity, TPrimaryKey, TOrmWhere> repository, IAspireMapper aspireMapper)
        {
            this.repository = repository;
            this.aspireMapper = aspireMapper;
        }

        /// <inheritdoc />
        public virtual async Task<TPrimaryKey?> CreateAsync(TCreateInputDto input)
        {
            var entity = MapToEntity(input);
            return await repository.CreateAsync(entity);
        }

        /// <inheritdoc />
        [HttpDelete("{primaryKey}")]
        public virtual async Task<int> DeleteAsync([Required] TPrimaryKey primaryKey)
        {
            return await repository.DeleteAsync(primaryKey);
        }

        /// <inheritdoc />
        [HttpGet("{primaryKey}")]
        public virtual async Task<TOutputDto?> GetAsync([Required] TPrimaryKey primaryKey)
        {
            var entity = await repository.GetAsync(primaryKey);
            if (entity == null) return default;

            return MapToDto(entity);
        }

        /// <inheritdoc />
        [HttpPut("{primaryKey}")]
        public virtual async Task<int> UpdateAsync([Required] TPrimaryKey primaryKey, [FromBody] TUpdateInputDto input)
        {
            var item = await repository.GetAsync(primaryKey);

            if (item is null) throw new FriendlyException("更新失败, 数据不存在");

            MapToEntity(input, ref item);
            return await repository.UpdateAsync(primaryKey, item);
        }

        /// <summary>
        ///     分页查询.
        /// </summary>
        /// <param name="input">查询过滤.</param>
        /// <param name="index">页索引.</param>
        /// <param name="size">页大小.</param>
        /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
        public virtual async Task<PagingOutputDto<TOutputDto>> PagingQueryAsync(
            [FromBody] TQueryFilterDto input,
            [Required] int index = 1,
            [Required] int size = 10)
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
            return aspireMapper.MapTo<TOutputDto>(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        /// <summary>
        ///     集体集合映射到dto集合.
        /// </summary>
        /// <param name="entities">实体集合.</param>
        /// <returns>dto集合.</returns>
        protected IEnumerable<TOutputDto> MapToDto(IEnumerable<TEntity> entities)
        {
            if (entities?.Any() != true) return Enumerable.Empty<TOutputDto>();

            return entities.Select(x => aspireMapper.MapTo<TOutputDto>(x));
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
            return aspireMapper.MapTo<TEntity>(updateInputDto ?? throw new ArgumentNullException(nameof(updateInputDto)));
        }

        /// <summary>
        ///     更新dto映射到实体.
        /// </summary>
        /// <param name="updateInputDto">更新dto.</param>
        /// <param name="outEntity"></param>
        /// <returns>实体.</returns>
        protected void MapToEntity(TUpdateInputDto updateInputDto, ref TEntity outEntity)
        {
            if (updateInputDto is null) throw new ArgumentNullException(nameof(updateInputDto));
            aspireMapper.MapTo<TUpdateInputDto, TEntity>(updateInputDto, ref outEntity);
        }
    }
}