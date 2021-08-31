using Aspire.Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.AppService.Auths
{
    /// <summary>
    ///     认证的 服务.
    /// </summary>
    public class AuthAppService : AppServiceBase
    {
        private readonly IAccountManage<IAccount> accountManage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthAppService" /> class.
        /// </summary>
        /// <param name="accountManage">账户管理.</param>
        public AuthAppService(IAccountManage<IAccount> accountManage)
        {
            this.accountManage = accountManage;
        }

        /// <summary>
        ///     获取token.
        /// </summary>
        /// <param name="accountId">账户Id.</param>
        /// <param name="password">密码.</param>
        /// <returns>token value.</returns>
        [Auth("admin")]
        public string GetToken(string accountId, string password)
        {
            // TODO 参数验证
            var account = accountManage.GetAccountByIdAndPassword(accountId, password);
            return accountManage.GetTokenByAccount(account);
        }

        /// <summary>
        ///     获取当前账户信息.
        /// </summary>
        /// <param name="account">账户(来自于 di).</param>
        /// <returns>当前账户.</returns>
        [Auth]
        public IAccount GetAccount([FromServices] IAccount account)
        {
            return account;
        }
    }
}