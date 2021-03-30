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
    public class IdentityAppService<TUser, TPrimaryKey> :
        Application,
        IIdentityAppService<IdentityLoginDto, IdentityTokenDto, CurrentUserDto>
        where TUser : IUser<TPrimaryKey>, IDomainService, new()
    {
        private readonly IUserManage<TUser, TPrimaryKey> userManage;
        private readonly IHttpContextAccessor httpContext;
        private readonly IAspireMapper mapper;
        private readonly IdentityAppSetting appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAppService{TUser,TPrimaryKey}"/> class.
        /// </summary>
        /// <param name="userManage">User Manage.</param>
        /// <param name="appSetting">App Setting.</param>
        /// <param name="httpContext">Http Context Accessor.</param>
        /// <param name="mapper">Mapper.</param>
        public IdentityAppService(
            IUserManage<TUser, TPrimaryKey> userManage,
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
            ICurrentUser user = null;

            if (input.Account == this.appSetting.Administrator.Account && input.Password == this.appSetting.Administrator.Password)
            {
                user = new TUser
                {
                    Account = input.Account,
                    Name = "管理员",
                    Roles = new[] { Roles.Admin },
                    Icon = "https://assets.gitee.com/assets/homepage/202006/illus_03clip-82894d0915ef30ae21667315c25fa56b.png",
                };
                goto Success;
            }

            user = this.userManage.GetByAccountAndPassword(input.Account, input.Password);

            if (user is not null)
            {
                goto Success;
            }

            throw Failure(ResponseCode.UnauthorizedAccountOrPassword, "用户名和密码无效.");

        Success:
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
