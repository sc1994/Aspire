using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspire.Dto;
using Aspire.Entity;
using Aspire.Interfaces;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable SA1402
namespace Aspire
{
    /// <summary>
    ///     app service crud 继承此类 将会在自动在接口中暴露出单表的crud接口.
    /// </summary>
    /// <typeparam name="TEntity">实体.</typeparam>
    /// <typeparam name="TOrmWhere">orm where.</typeparam>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TQueryFilterDto">查询过滤dto.</typeparam>
    /// <typeparam name="TOutputDto">输出dto.</typeparam>
    /// <typeparam name="TCreateInputDto">创建输入dto.</typeparam>
    /// <typeparam name="TUpdateInputDto">更新输入dto.</typeparam>
    // ReSharper disable once SA1402
    public abstract class AppServiceCrud<
        TEntity,
        TOrmWhere,
        TPrimaryKey,
        TQueryFilterDto,
        TOutputDto,
        TCreateInputDto,
        TUpdateInputDto> : AppServiceBase,
        ICrudSingle<TPrimaryKey, TCreateInputDto, TOutputDto, TUpdateInputDto>
        where TEntity : IEntityBase<TPrimaryKey>
    {
        private readonly IAspireMapper aspireMapper;
        private readonly IRepository<TEntity, TOrmWhere, TPrimaryKey> repository;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="AppServiceCrud{        TEntity,         TOrmWhere,         TPrimaryKey,         TQueryFilterDto,         TOutputDto,         TCreateInputDto,         TUpdateInputDto}" />
        ///     class.
        /// </summary>
        /// <param name="repository">仓储实例.</param>
        /// <param name="aspireMapper">mapper实例.</param>
        protected AppServiceCrud(IRepository<TEntity, TOrmWhere, TPrimaryKey> repository, IAspireMapper aspireMapper)
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
        /// <param name="index">索引.</param>
        /// <param name="size">大小.</param>
        /// <param name="input">查询过滤.</param>
        /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
        [HttpPost("{index}_{size}")] // TODO 默认值
        public virtual async Task<PagingOutputDto<TOutputDto>> PagingQueryAsync(
            int index,
            int size,
            TQueryFilterDto input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var (totalCount, list) =
                await repository.PagingListAsync(QueryFilter(input), index, size);

            var dtos = MapToDtos(list);

            return new PagingOutputDto<TOutputDto>(totalCount, dtos);
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
        protected IEnumerable<TOutputDto> MapToDtos(IEnumerable<TEntity> entities)
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
            return aspireMapper.MapTo<TEntity>(createInputDto);
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