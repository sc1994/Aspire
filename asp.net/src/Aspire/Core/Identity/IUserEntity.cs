// <copyright file="IUserEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System;

    /// <summary>
    /// 用户 实体.
    /// </summary>
    public interface IUserEntity : IUserEntity<Guid>
    {
    }

    /// <summary>
    /// 用户 实体.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public interface IUserEntity<TPrimaryKey> : IAuditEntity<TPrimaryKey>
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
