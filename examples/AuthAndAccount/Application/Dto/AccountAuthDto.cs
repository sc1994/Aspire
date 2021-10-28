using AuthAndAccount.Core.Accounts;

namespace AuthAndAccount.Application
{
    public class AccountAuthDto : Account
    {
        public string ExtendField { get; set; } = "xxx";
    }
}