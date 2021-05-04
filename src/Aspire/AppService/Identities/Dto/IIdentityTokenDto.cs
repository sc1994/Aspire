// <copyright file="IIdentityTokenDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
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
        string Token { get; set; }

        /// <summary>
        /// Gets or sets 到期时间.
        /// </summary>
        DateTime ExpiryTime { get; set; }

        /// <summary>
        /// Gets or sets Ttl.
        /// </summary>
        int Ttl { get; set; }

        /// <summary>
        /// Gets or sets Token Header Name.
        /// </summary>
        public string TokenHeaderName { get; set; }
    }
}