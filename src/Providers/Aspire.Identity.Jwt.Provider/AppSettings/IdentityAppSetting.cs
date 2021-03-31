// <copyright file="IdentityAppSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity.Jwt.Provider.AppSettings
{
    /// <summary>
    /// Identity 配置项.
    /// </summary>
    public class IdentityAppSetting
    {
        /// <summary>
        /// Gets or sets Secret.
        /// </summary>
        internal string Secret { get; set; }

        /// <summary>
        /// Gets or sets Audience.
        /// </summary>
        internal string ValidAudience { get; set; }

        /// <summary>
        /// Gets or sets Issuer.
        /// </summary>
        internal string ValidIssuer { get; set; }

        /// <summary>
        /// Gets or sets 到期秒.
        /// </summary>
        internal int ExpireSeconds { get; set; }

        /// <summary>
        /// Gets or sets 管理员.
        /// </summary>
        internal AdministratorAppSetting Administrator { get; set; }
    }
}
