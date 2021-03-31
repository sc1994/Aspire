// <copyright file="ISystemLogCommonDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLogs
{
    using Newtonsoft.Json;

    /// <summary>
    /// 系统日志 通用实体.
    /// </summary>
    public interface ISystemLogCommonDto
    {
        /// <summary>
        /// Gets or sets api method.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string ApiMethod { get; set; }

        /// <summary>
        /// Gets or sets api router.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string ApiRouter { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string Title { get; set; }

        /// <summary>
        /// Gets or sets traceId.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string TraceId { get; set; }

        /// <summary>
        /// Gets or sets 过滤1.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string Filter1 { get; set; }

        /// <summary>
        /// Gets or sets 过滤2.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string Filter2 { get; set; }

        /// <summary>
        /// Gets or sets 远端 地址.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string ClientAddress { get; set; }

        /// <summary>
        /// Gets or sets 服务端 地址.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string ServerAddress { get; set; }

        /// <summary>
        /// Gets or sets 等级.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string Level { get; set; }
    }
}
