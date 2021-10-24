using Aspire;
using Aspire.Domain.Account;
using AuthAndAccount.Domain;

namespace AuthAndAccount.Core.Accounts
{
    public class AccountManage : IAccountManage<Account>
    {
        public Account GetAccountByIdAndPassword(string accountId, string password)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetTokenByAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}