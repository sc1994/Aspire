// <copyright file="IUserBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    /// <summary>
    /// 用户基础.
    /// </summary>
    public interface IUserBase
    {
        /// <summary>
        /// Gets or sets 姓名.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets 角色.
        /// </summary>
        string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets Icon.
        /// </summary>
        string Icon { get; set; }
    }
}