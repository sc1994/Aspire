// <copyright file="ISystemLogAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLogs
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// System Log.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Primary Key.</typeparam>
    /// <typeparam name="TFilterInputDto">Filter Input Dto.</typeparam>
    /// <typeparam name="TFilterOutputDto">Filter Output Dto.</typeparam>
    /// <typeparam name="TDetailOutputDto">Detail Output Dto.</typeparam>
    [Authentication(Roles.Admin)]
    [IgnoreActionLog]
    public interface ISystemLogAppService<
        in TPrimaryKey,
        in TFilterInputDto,
        TFilterOutputDto,
        TDetailOutputDto> : IApplication
        where TFilterInputDto : ISystemLogFilterInputDto
        where TFilterOutputDto : ISystemLogFilterOutputDto<TPrimaryKey>
        where TDetailOutputDto : ISystemLogDetailOutputDto<TPrimaryKey>
    {
        /// <summary>
        /// filter.
        /// </summary>
        /// <param name="filterInput">Filter Input.</param>
        /// <returns>分页过滤输出.</returns>
        Task<PagedResultDto<TFilterOutputDto>> FilterAsync(TFilterInputDto filterInput);

        /// <summary>
        /// Get Detail.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <returns>详情输出.</returns>
        Task<TDetailOutputDto> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 获取选择项.
        /// </summary>
        /// <returns>选择项集合.</returns>
        Task<SystemLogSelectItemsDto> GetSelectItems();

        /// <summary>
        /// 删除全部选择项.
        /// </summary>
        /// <returns>Is Success.</returns>
        Task<bool> DeleteAllSelectItems();

        /// <summary>
        /// Get Page Config.
        /// </summary>
        /// <returns>Config.</returns>
        Task<JObject> GetPageConfig();
    }
}
