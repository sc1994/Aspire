// <copyright file="IdentityAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identities
{
    using System.Threading.Tasks;
    using Aspire.Identity.Jwt.Provider;
    using Aspire.Identity.Jwt.Provider.AppSettings;
    using Aspire.Mapper;
    using Aspire.Users;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    /// <inheritdoc cref="IIdentityAppService{TLoginDto,TIdentityTokenDto,TCurrentUserDto}"/>
    public class IdentityAppService<TUser> :
        Application,
        IIdentityAppService<IdentityLoginDto, IdentityTokenDto, CurrentUserDto>
        where TUser : IUser, IDomainService
    {
        private readonly IUserManage<TUser> userManage;
        private readonly IHttpContextAccessor httpContext;
        private readonly IAspireMapper mapper;
        private readonly IdentityAppSetting appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAppService{TUser}"/> class.
        /// </summary>
        /// <param name="userManage">User Manage.</param>
        /// <param name="appSetting">App Setting.</param>
        /// <param name="httpContext">Http Context Accessor.</param>
        /// <param name="mapper">Mapper.</param>
        public IdentityAppService(
            IUserManage<TUser> userManage,
            IOptions<IdentityAppSetting> appSetting,
            IHttpContextAccessor httpContext,
            IAspireMapper mapper)
        {
            this.userManage = userManage;
            this.httpContext = httpContext;
            this.mapper = mapper;
            this.appSetting = appSetting.Value;
        }

        /// <inheritdoc />
        [IgnoreAuthentication]
        public async Task<IdentityTokenDto> LoginAsync(IdentityLoginDto input)
        {
            var user = this.userManage.GetByAccountAndPassword(input.Account, input.Password);

            if (user == null)
            {
                throw Failure(ResponseCode.UnauthorizedAccountOrPassword, "用户名和密码无效.");
            }

            return await Task.FromResult(JwtManage.GenerateJwtToken(user, this.appSetting));
        }

        /// <inheritdoc />
        public async Task<CurrentUserDto> GetCurrentUserAsync()
        {
            if (this.httpContext.HttpContext?.Items["TODO"] is ICurrentUser user)
            {
                return await Task.FromResult(this.mapper.MapTo<CurrentUserDto>(user));
            }

            throw Failure(ResponseCode.AuthorizeInvalid, "token无效");
        }

        /// <inheritdoc />
        public async Task<bool> LogoutAsync()
        {
            return await Task.FromResult(true);
        }
    }
}
