// <copyright file="IdentityTokenDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    using System;

    /// <inheritdoc />
    public class IdentityTokenDto : IIdentityTokenDto
    {
        /// <inheritdoc />
        public string Token { get; set; }

        /// <inheritdoc />
        public DateTime ExpiryTime { get; set; }

        /// <inheritdoc />
        public int Ttl { get; set; }

        /// <inheritdoc />
        public string TokenHeaderName { get; set; }
    }
}