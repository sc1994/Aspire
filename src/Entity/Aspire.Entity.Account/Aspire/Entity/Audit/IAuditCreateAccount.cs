namespace Aspire.Entity.Audit
{
    /// <summary>
    ///     创建审计.
    /// </summary>
    public interface IAuditCreateAccount
    {
        /// <summary>
        ///     Gets or sets 创建者.
        /// </summary>
        string Creator { get; set; }
    }
}