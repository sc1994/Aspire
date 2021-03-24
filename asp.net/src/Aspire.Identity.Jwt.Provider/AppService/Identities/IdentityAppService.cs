// <copyright file="IdentityAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    using System;
    using System.Threading.Tasks;

    /// <inheritdoc cref="IIdentityAppService{TLoginDto,TIdentityTokenDto,TCurrentUserDto}"/>
    public class IdentityAppService :
        Application,
        IIdentityAppService<IdentityLoginDto, IdentityTokenDto, CurrentUserDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAppService"/> class.
        /// </summary>
        public IdentityAppService()
        {
        }

        /// <inheritdoc />
        public async Task<IdentityTokenDto> LoginAsync(IdentityLoginDto input)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<CurrentUserDto> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
