// <copyright file="IUserRole.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using System;

    /// <summary>
    /// 用户角色.
    /// </summary>
    public interface IUserRole : IUserRole<Guid>
    {
    }

    /// <summary>
    /// 用户角色.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public interface IUserRole<TPrimaryKey> : IAuditEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets 角色名.
        /// </summary>
        string RoleName { get; set; }
    }
}