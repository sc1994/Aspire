namespace Aspire.Domain.Account
{
    /// <summary>
    ///     账户 管理.
    /// </summary>
    /// <typeparam name="TAccount">账户.</typeparam>
    public interface IAccountManage<TAccount>
        where TAccount : class, IAccount
    {
        /// <summary>
        ///     根据Id和密码 获取账户.
        /// </summary>
        /// <param name="accountId">账户Id.</param>
        /// <param name="password">密码.</param>
        /// <returns>当前账户.</returns>
        TAccount GetAccountByIdAndPassword(string accountId, string password);

        /// <summary>
        ///     根据账户信息 获取 token.
        /// </summary>
        /// <param name="account">账户.</param>
        /// <returns>token.</returns>
        string GetTokenByAccount(TAccount account);

        /// <summary>
        ///     根据token 获取账户.
        /// </summary>
        /// <param name="token">token.</param>
        /// <returns>账户.</returns>
        TAccount GetAccountByToken(string token);
    }
}