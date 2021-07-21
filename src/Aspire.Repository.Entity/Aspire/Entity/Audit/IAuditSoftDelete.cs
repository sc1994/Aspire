using System;

namespace Aspire.Entity.Audit
{
    /// <summary>
    ///     软删除审计.
    /// </summary>
    public interface IAuditSoftDelete
    {
        /// <summary>
        ///     Gets or sets a value indicating whether 是否删除.
        /// </summary>
        bool Deleted { get; set; }

        /// <summary>
        ///     Gets or sets 删除时间.
        /// </summary>
        DateTime DeletedAt { get; set; }

        /// <summary>
        ///     Gets or sets 删除人.
        /// </summary>
        string Deleter { get; set; }
    }
}