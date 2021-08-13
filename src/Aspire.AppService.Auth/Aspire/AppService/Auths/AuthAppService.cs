using System;
using Aspire.Core.Account;
using Aspire.Core.Jwt;

namespace Aspire.AppService.Auths
{
    /// <summary>
    /// 认证的 服务.
    /// </summary>
    public class AuthAppService : AppServiceBase
    {
        private readonly IAccountManage accountManage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthAppService"/> class.
        /// </summary>
        /// <param name="accountManage">账户管理.</param>
        public AuthAppService(IAccountManage accountManage)
        {
            this.accountManage = accountManage;
        }

        /// <summary>
        /// 获取token.
        /// </summary>
        /// <param name="accountId">账户Id.</param>
        /// <param name="password">密码.</param>
        /// <returns>token value.</returns>
        public string GetToken(string accountId, string password)
        {
            if (accountId == null) throw new ArgumentNullException(nameof(accountId));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var account = accountManage.GetAccountByIdAndPassword(accountId, password);
            return JwtManage.Create(account);
        }

        /// <summary>
        /// 获取当前账户信息.
        /// </summary>
        /// <returns>当前账户.</returns>
        public IAccount GetCurrentAccount()
        {
            throw new NotImplementedException();
        }
    }
}