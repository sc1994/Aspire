// <copyright file="IUserUpdateDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    /// <inheritdoc cref="IUserBase"/>
    /// <inheritdoc cref="IDto{TPrimaryKey}"/>
    public interface IUserUpdateDto<TPrimaryKey> : IUserBase, IDto<TPrimaryKey>
    {
    }
}