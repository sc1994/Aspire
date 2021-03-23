// <copyright file="SystemLogCommonDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLog
{
    /// <summary>
    /// Common Dto.
    /// </summary>
    public class SystemLogCommonDto : ISystemLogCommonDto
    {
        /// <inheritdoc/>
        public virtual string ApiMethod { get; set; }

        /// <inheritdoc/>
        public virtual string ApiRouter { get; set; }

        /// <inheritdoc/>
        public virtual string Title { get; set; }

        /// <inheritdoc/>
        public virtual string TraceId { get; set; }

        /// <inheritdoc/>
        public virtual string Filter1 { get; set; }

        /// <inheritdoc/>
        public virtual string Filter2 { get; set; }

        /// <inheritdoc/>
        public virtual string ClientAddress { get; set; }

        /// <inheritdoc/>
        public virtual string ServerAddress { get; set; }

        /// <inheritdoc/>
        public virtual string Level { get; set; }
    }
}
