namespace Aspire
{
    /// <summary>
    ///     当前用户.
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        ///     Gets or sets 账户Id(唯一键).
        /// </summary>
        string AccountId { get; set; }

        /// <summary>
        ///     Gets or sets name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets 角色.
        /// </summary>
        string[] Roles { get; set; }
    }
}