// <copyright file="IUserManage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using System;

    /// <summary>
    /// User Manage.
    /// </summary>
    /// <typeparam name="TUser">User.</typeparam>
    public interface IUserManage<out TUser> : IUserManage<TUser, Guid>
        where TUser : IUser, IDomainService
    {
    }

    /// <summary>
    /// User Manage.
    /// </summary>
    /// <typeparam name="TUser">User.</typeparam>
    /// <typeparam name="TPrimaryKey">Primary Key.</typeparam>
    public interface IUserManage<out TUser, TPrimaryKey>
        where TUser : IUser<TPrimaryKey>, IDomainService
    {
        /// <summary>
        /// 根据账号获取用户.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <returns>User Entity.</returns>
        TUser GetByAccount(string account);

        /// <summary>
        /// 根据用户名和密码获取.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <param name="password">Password.</param>
        /// <returns>User Entity.</returns>
        TUser GetByAccountAndPassword(string account, string password);

        /// <summary>
        /// Encrypt Password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>加密后密码.</returns>
        string EncryptPassword(string password);
    }
}
