// <copyright file="IUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using System;
    using Aspire.Identities;

    /// <summary>
    /// 用户 实体.
    /// </summary>
    public interface IUser : IUser<Guid>
    {
    }

    /// <summary>
    /// 用户.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    public interface IUser<TPrimaryKey> : IAuditEntity<TPrimaryKey>, ICurrentUser, IUserPassword
    {
    }
}
