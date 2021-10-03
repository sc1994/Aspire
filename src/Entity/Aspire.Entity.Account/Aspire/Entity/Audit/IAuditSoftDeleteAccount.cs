namespace Aspire.Entity.Audit
{
    /// <summary>
    ///     软删除审计.
    /// </summary>
    public interface IAuditSoftDeleteAccount
    {
        /// <summary>
        ///     Gets or sets 删除人.
        /// </summary>
        string Deleter { get; set; }
    }
}