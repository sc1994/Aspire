// <copyright file="SystemLogSelectItemsDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLogs
{
    using System;

    /// <summary>
    /// 选项集合.
    /// </summary>
    public class SystemLogSelectItemsDto
    {
        /// <summary>
        /// Gets or sets API路由.
        /// </summary>
        public string[] ApiMethod { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets API方法.
        /// </summary>
        public string[] ApiRouter { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets 过滤1.
        /// </summary>
        public string[] Filter1 { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets 过滤2.
        /// </summary>
        public string[] Filter2 { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets 客户端地址.
        /// </summary>
        public string[] ClientAddress { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets 服务端地址.
        /// </summary>
        public string[] ServerAddress { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string[] Title { get; set; }
    }
}
