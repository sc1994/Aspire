// <copyright file="AdministratorAppSetting.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity.Jwt.Provider.AppSettings
{
    /// <summary>
    /// 管理员 配置项.
    /// </summary>
    public class AdministratorAppSetting
    {
        /// <summary>
        /// Gets or sets 主键.
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets 用户名.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets 密码.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }
    }
}