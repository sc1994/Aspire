using Aspire;
using Aspire.Application.Auths;
using Aspire.Domain.Account;
using AuthAndAccount.Core.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AuthAndAccount.Application
{
    public class AuthApplication : AuthApplication<Account>
    {
        public AuthApplication(IAccountManage<Account> accountManage) : base(accountManage)
        {
        }

        [Auth("admin")]
        public DateTime GetTime()
        {
            return DateTime.Now;
        }

        public override Account GetAccount([FromServices] Account account)
        {
            var item = base.GetAccount(account);

            return new AccountAuthDto();
        }
    }
}