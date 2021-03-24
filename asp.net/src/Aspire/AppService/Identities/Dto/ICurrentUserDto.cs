// <copyright file="ICurrentUserDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    /// <summary>
    /// Current User Dto.
    /// </summary>
    public interface ICurrentUserDto : ICurrentUser
    {
        /// <summary>
        /// Gets or sets Icon.
        /// </summary>
        string Icon { get; set; }
    }
}