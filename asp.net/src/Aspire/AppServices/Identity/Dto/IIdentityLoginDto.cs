// <copyright file="IIdentityLoginDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    /// <summary>
    /// Login Dto.
    /// </summary>
    public interface IIdentityLoginDto
    {
        /// <summary>
        /// Gets or sets 用户Id.
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// Gets or sets 密码.
        /// </summary>
        string Password { get; set; }
    }
}
