// <copyright file="IIdentityTokenDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System;

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