// <copyright file="IUserCreateDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    using Aspire.Identities;

    /// <inheritdoc cref="ICurrentUser"/>
    /// <inheritdoc cref="IUserPassword"/>
    public interface IUserCreateDto : ICurrentUser, IUserPassword
    {
    }
}