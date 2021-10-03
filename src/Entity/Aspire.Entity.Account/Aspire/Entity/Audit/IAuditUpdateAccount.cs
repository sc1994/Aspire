namespace Aspire.Entity.Audit
{
    /// <summary>
    ///     更新审计.
    /// </summary>
    public interface IAuditUpdateAccount
    {
        /// <summary>
        ///     Gets or sets 更新人.
        /// </summary>
        string Updater { get; set; }
    }
}