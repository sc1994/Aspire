// <copyright file="IdentityAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System;
    using System.Threading.Tasks;
    using Aspire.UserAdmin;

    /// <inheritdoc />
    public class IdentityAppService : Application,
        IIdentityAppService<IdentityLoginDto, IdentityTokenDto, CurrentUserDto>,
        IUserAdminAppService<IUserAdmin,>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAppService"/> class.
        /// </summary>
        /// <param name="userManage">User Manage.</param>
        public IdentityAppService(UserAdminAppService userManage)
        {
            this.userManage = userManage;
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
