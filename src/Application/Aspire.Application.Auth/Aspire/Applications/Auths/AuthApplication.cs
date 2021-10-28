using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Aspire.Domain.Account;

using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.Auths
{
    /// <summary>
    ///     认证的 服务.
    /// </summary>
    /// <typeparam name="TAccount">账户数据类型.</typeparam>
    public abstract class AuthApplication<TAccount> : ApplicationBase
        where TAccount : class, IAccount, new()
    {
        private readonly IAccountManage<TAccount> accountManage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthApplication{TAccount}" /> class.
        /// </summary>
        /// <param name="accountManage">账户管理.</param>
        public AuthApplication(IAccountManage<TAccount> accountManage)
        {
            this.accountManage = accountManage;
        }

        /// <summary>
        ///     获取token.
        /// </summary>
        /// <param name="accountId">账户Id.</param>
        /// <param name="password">密码.</param>
        /// <returns>token value.</returns>
        public virtual async Task<string> GetTokenAsync([Required] string accountId, [Required] string password)
        {
            var account = await accountManage.GetAccountByIdAndPasswordAsync(accountId, password);
            return await accountManage.GetTokenByAccountAsync(account);
        }

        /// <summary>
        ///     获取当前账户信息.
        /// </summary>
        /// <param name="account">账户(来自于 di).</param>
        /// <returns>当前账户.</returns>
        [Auth]
        public virtual TAccount GetAccount([FromServices] TAccount account)
        {
            return account;
        }
    }
}