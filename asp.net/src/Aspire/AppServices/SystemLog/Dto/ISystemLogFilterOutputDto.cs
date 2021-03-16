// <copyright file="ISystemLogFilterOutputDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLog
{
    using Newtonsoft.Json;

    /// <summary>
    /// 系统日志 过滤 输出.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Primary Key.</typeparam>
    public interface ISystemLogFilterOutputDto<TPrimaryKey> : ISystemLogCommonDto
    {
        /// <summary>
        /// Gets or sets 创建时间.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets Messages.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string Message { get; set; }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        TPrimaryKey Id { get; set; }
    }
}
