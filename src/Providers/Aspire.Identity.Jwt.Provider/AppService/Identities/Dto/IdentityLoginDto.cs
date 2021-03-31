// <copyright file="IdentityLoginDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    /// <inheritdoc />
    public class IdentityLoginDto : IIdentityLoginDto
    {
        /// <inheritdoc />
        public string Account { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }
    }
}