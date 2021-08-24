using Aspire;
using Aspire.Domain.Account;

namespace template_1qaz2wsx3edc.Domain.Accounts
{
    public class AccountManage : IAccountManage<IAccount>
    {
        public IAccount GetAccountByIdAndPassword(string accountId, string password)
        {
            return new Account();
        }

        public string GetTokenByAccount(IAccount account)
        {
            return "1234";
        }

        public IAccount GetAccountByToken(string token)
        {
            return new Account();
        }
    }
}
