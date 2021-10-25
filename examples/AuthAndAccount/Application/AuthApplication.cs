using Aspire.Application.Auths;
using Aspire.Domain.Account;
using AuthAndAccount.Core.Accounts;

namespace AuthAndAccount.Application
{
    public class AuthApplication : AuthApplication<Account>
    {
        public AuthApplication(IAccountManage<Account> accountManage) : base(accountManage)
        {
        }
    }
}