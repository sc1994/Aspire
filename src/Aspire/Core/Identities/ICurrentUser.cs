// <copyright file="ICurrentUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    using Aspire.Users;

    /// <summary>
    /// 当前登入用户.
    /// </summary>
    public interface ICurrentUser : IUserAccount, IUserBase
    {
    }
}
