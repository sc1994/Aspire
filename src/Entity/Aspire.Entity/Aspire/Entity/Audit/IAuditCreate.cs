using System;

namespace Aspire.Entity.Audit
{
    /// <summary>
    ///     创建审计.
    /// </summary>
    public interface IAuditCreate
    {
        /// <summary>
        ///     Gets or sets 创建时间.
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}