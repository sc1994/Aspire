// <copyright file="ILoginDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System;

    /// <summary>
    /// Login Dto.
    /// </summary>
    public interface ILoginDto
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

    /// <summary>
    /// Identity Token Dto.
    /// </summary>
    public interface IIdentityTokenDto
    {
        /// <summary>
        /// Gets or sets Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets 到期时间.
        /// </summary>
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// Gets or sets Ttl.
        /// </summary>
        public int Ttl { get; set; }
    }
}
