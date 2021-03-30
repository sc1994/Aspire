// <copyright file="IPageInputDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    /// <summary>
    /// 页输入.
    /// </summary>
    public interface IPageInputDto
    {
        /// <summary>
        /// Gets or sets 页 索引.
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets 页 大小.
        /// </summary>
        int PageSize { get; set; }
    }
}