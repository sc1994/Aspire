// <copyright file="IUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using System;

    /// <summary>
    /// 用户 实体.
    /// </summary>
    public interface IUser : IUser<Guid>
    {
    }

    /// <summary>
    /// 用户
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public interface IUser<TPrimaryKey> : IAuditEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets 用户Id.
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// Gets or sets 姓名.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets 密码.
        /// </summary>
        string Password { get; set; }
    }
}
