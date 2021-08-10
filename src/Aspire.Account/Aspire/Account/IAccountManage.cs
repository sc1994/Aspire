namespace Aspire.Account
{
    /// <summary>
    /// 账户 管理.
    /// </summary>
    public interface IAccountManage
    {
        /// <summary>
        /// 获取账户.
        /// </summary>
        /// <param name="params">参数集合(比如使用账户Id+密码获取账户等).</param>
        /// <returns>当前账户.</returns>
        public abstract ICurrentAccount GetAccount(params string[] @params);
    }
}