// <copyright file="IIdentityAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 身份服务.
    /// </summary>
    /// <typeparam name="TLoginDto">Login Dto.</typeparam>
    /// <typeparam name="TIdentityTokenDto">Identity Token Dto.</typeparam>
    /// <typeparam name="TCurrentUserDto">Current User Dto.</typeparam>
    public interface IIdentityAppService<
        in TLoginDto,
        TIdentityTokenDto,
        TCurrentUserDto> : IApplication
        where TLoginDto : IIdentityLoginDto
        where TCurrentUserDto : ICurrentUserDto
    {
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="input">Login Dto.</param>
        /// <returns>Identity Token Dto.</returns>
        [HttpPost]
        Task<TIdentityTokenDto> LoginAsync(TLoginDto input);

        /// <summary>
        /// Get Current User.
        /// </summary>
        /// <returns>Current User Dto.</returns>
        Task<TCurrentUserDto> GetCurrentUserAsync();

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns>Boolean.</returns>
        [HttpGet]
        Task<bool> LogoutAsync();
    }
}
