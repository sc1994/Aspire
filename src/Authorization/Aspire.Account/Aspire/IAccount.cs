﻿namespace Aspire
{
    /// <summary>
    ///     账户.
    /// </summary>
    public interface IAccount
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