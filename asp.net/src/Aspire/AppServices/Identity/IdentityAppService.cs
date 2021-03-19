// <copyright file="IdentityAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 身份服务.
    /// </summary>
    /// <typeparam name="TIdentityEntity">Audit Entity.</typeparam>
    /// <typeparam name="TPrimaryKey">Primary Key.</typeparam>
    /// <typeparam name="TPageInputDto">Page Input Dto.</typeparam>
    /// <typeparam name="TOutputDto">Output Dto.</typeparam>
    /// <typeparam name="TCreateDto">Create Dto.</typeparam>
    /// <typeparam name="TUpdateDto">Update Dto.</typeparam>
    /// <typeparam name="TLoginDto">Login Dto.</typeparam>
    /// <typeparam name="TIdentityTokenDto">Identity Token Dto.</typeparam>
    /// <typeparam name="TCurrentUserDto">Current User Dto.</typeparam>
    public abstract class IdentityAppService<
        TIdentityEntity,
        TPrimaryKey,
        TPageInputDto,
        TOutputDto,
        TCreateDto,
        TUpdateDto,
        TLoginDto,
        TIdentityTokenDto,
        TCurrentUserDto> : CrudApplication<
        TIdentityEntity,
        TPrimaryKey,
        TPageInputDto,
        TOutputDto,
        TCreateDto,
        TUpdateDto>
        where TIdentityEntity : IAuditEntity<TPrimaryKey>
        where TPageInputDto : PageInputDto
        where TUpdateDto : IDto<TPrimaryKey>
        where TLoginDto : ILoginDto
        where TCurrentUserDto : ICurrentUserDto
    {
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="input">Login Dto.</param>
        /// <returns>Identity Token Dto.</returns>
        [HttpPost]
        public abstract Task<TIdentityTokenDto> Login(TLoginDto input);

        /// <summary>
        /// Get Current User.
        /// </summary>
        /// <returns>Current User Dto.</returns>
        public abstract Task<TCurrentUserDto> GetCurrentUser();

        /// <summary>
        /// Logout.
        /// </summary>
        /// <returns>Boolean.</returns>
        [HttpGet]
        public abstract Task<bool> Logout();
    }
}
