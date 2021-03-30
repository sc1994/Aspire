// <copyright file="PageInputDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    /// <inheritdoc />
    public class PageInputDto : IPageInputDto
    {
        /// <inheritdoc />
        public int PageIndex { get; set; } = 1;

        /// <inheritdoc />
        public int PageSize { get; set; } = 10;
    }
}
