// <copyright file="ICrudApplication.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Threading.Tasks;

    /// <inheritdoc />
    public interface ICrudApplication<
        TAuditEntity> : ICrudApplication<
        TAuditEntity,
        Guid>
        where TAuditEntity : IAuditEntity
    {
    }

    /// <inheritdoc />
    public interface ICrudApplication<
        TAuditEntity,
        in TPrimaryKey> : ICrudApplication<
        TAuditEntity,
        TPrimaryKey,
        PageInputDto>
        where TAuditEntity : IAuditEntity<TPrimaryKey>
    {
    }

    /// <inheritdoc />
    public interface ICrudApplication<
        TAuditEntity,
        in TPrimaryKey,
        in TPageInputDto> : ICrudApplication<
        TAuditEntity,
        TPrimaryKey,
        TPageInputDto,
        TAuditEntity,
        TAuditEntity>
        where TAuditEntity : IAuditEntity<TPrimaryKey>
        where TPageInputDto : IPageInputDto
    {
    }

    /// <inheritdoc />
    public interface ICrudApplication<
        TAuditEntity,
        in TPrimaryKey,
        in TPageInputDto,
        TDto> : ICrudApplication<
        TAuditEntity,
        TPrimaryKey,
        TPageInputDto,
        TDto,
        TDto>
        where TAuditEntity : IAuditEntity<TPrimaryKey>
        where TPageInputDto : IPageInputDto
        where TDto : IDto<TPrimaryKey>
    {
    }

    /// <inheritdoc />
    public interface ICrudApplication<
        TAuditEntity,
        in TPrimaryKey,
        in TPageInputDto,
        TOutputDto,
        in TCreateOrUpdateDto> : ICrudApplication<
        TAuditEntity,
        TPrimaryKey,
        TPageInputDto,
        TOutputDto,
        TCreateOrUpdateDto,
        TCreateOrUpdateDto>
        where TAuditEntity : IAuditEntity<TPrimaryKey>
        where TPageInputDto : IPageInputDto
        where TCreateOrUpdateDto : IDto<TPrimaryKey>
    {
    }

    /// <summary>
    /// CRUD 服务.
    /// </summary>
    /// <typeparam name="TAuditEntity">数据库审计实体.</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键.</typeparam>
    /// <typeparam name="TPageInputDto">数据传输对象 分页输入.</typeparam>
    /// <typeparam name="TOutputDto">数据传输 输出对象.</typeparam>
    /// <typeparam name="TCreateDto">数据传输 创建对象.</typeparam>
    /// <typeparam name="TUpdateDto">数据传输 更新对象.</typeparam>
    public interface ICrudApplication<
        TAuditEntity,
        in TPrimaryKey,
        in TPageInputDto,
        TOutputDto,
        in TCreateDto,
        in TUpdateDto> : IApplication
        where TAuditEntity : IAuditEntity<TPrimaryKey>
        where TPageInputDto : IPageInputDto
        where TUpdateDto : IDto<TPrimaryKey>
    {
        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="input">Input Dto.</param>
        /// <returns>Output Dto.</returns>
        Task<TOutputDto> CreateAsync(TCreateDto input);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Is Success.</returns>
        Task<bool> DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>Output.</returns>
        Task<TOutputDto> UpdateAsync(TUpdateDto input);

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Output.</returns>
        Task<TOutputDto> GetAsync(TPrimaryKey id);

        /// <summary>
        /// Paging.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>Page Output.</returns>
        Task<PagedResultDto<TOutputDto>> PagingAsync(TPageInputDto input);
    }
}