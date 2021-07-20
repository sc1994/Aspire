using System;

namespace Aspire.Audit
{
    /// <summary>
    ///     更新审计.
    /// </summary>
    public interface IAuditUpdate
    {
        /// <summary>
        ///     Gets or sets 更新时间.
        /// </summary>
        DateTime UpdatedAt { get; set; }

        /// <summary>
        ///     Gets or sets 更新人.
        /// </summary>
        string Updater { get; set; }
    }
}