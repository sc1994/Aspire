using System;

namespace Aspire.Audit
{
    /// <summary>
    ///     创建审计.
    /// </summary>
    public interface IAuditCreate
    {
        /// <summary>
        ///     Gets or sets 创建者.
        /// </summary>
        string Creator { get; set; }

        /// <summary>
        ///     Gets or sets 创建时间.
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}