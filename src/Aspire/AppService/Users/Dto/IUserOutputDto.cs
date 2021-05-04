// <copyright file="IUserOutputDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using Aspire.Identities;

    /// <inheritdoc cref="ICurrentUser"/>
    public interface IUserOutputDto<TPrimaryKey> : ICurrentUser
    {
    }
}