// <copyright file="SystemLogFilterOutputDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLog
{
    using System;

    /// <summary>
    /// Filter Output.
    /// </summary>
    public class SystemLogFilterOutputDto : SystemLogCommonDto, ISystemLogFilterOutputDto<string>
    {
        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public string Message { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public long CreatedAtTicks => this.CreatedAt.Ticks;
    }
}
