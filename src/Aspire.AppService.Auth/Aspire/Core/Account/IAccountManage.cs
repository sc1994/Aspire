namespace Aspire.Core.Account
{
    /// <summary>
    /// 账户 管理.
    /// </summary>
    public interface IAccountManage
    {
        /// <summary>
        /// 根据Id和密码 获取账户.
        /// </summary>
        /// <param name="accountId">账户Id.</param>
        /// <param name="password">密码.</param>
        /// <returns>当前账户.</returns>
        IAccount GetAccountByIdAndPassword(string accountId, string password);
    }
}