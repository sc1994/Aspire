using Aspire;
using Aspire.Domain.Account;
using Aspire.Helpers;

namespace AuthAndAccount.Core.Accounts
{
    public class AccountManage : IAccountManage<Account>
    {
        public async Task<Account> GetAccountByIdAndPasswordAsync(string accountId, string password)
        {
            return await Task.FromResult(new Account
            {
                AccountId = "test",
                Roles = new[] { "admin" }
            });
        }

        public async Task<Account> GetAccountByTokenAsync(string token)
        {
            return await Task.FromResult(JwtHelper.ParseJwt<Account>(token, "123123123123123123123123123123123123123123123123"));
        }

        public async Task<string> GetTokenByAccountAsync(Account account)
        {
            return await Task.FromResult(JwtHelper.GenerateJwt(account, 60 * 60 * 24, "123123123123123123123123123123123123123123123123"));
        }
    }
}