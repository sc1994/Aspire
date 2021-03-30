// <copyright file="CurrentUserDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    /// <inheritdoc />
    public class CurrentUserDto : ICurrentUserDto
    {
        /// <inheritdoc />
        public string Account { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string[] Roles { get; set; }

        /// <inheritdoc />
        public string Icon { get; set; }
    }
}