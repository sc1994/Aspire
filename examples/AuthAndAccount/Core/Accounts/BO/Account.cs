using Aspire;

namespace AuthAndAccount.Core.Accounts
{
    public class Account : IAccount
    {
        public Account()
        {
            AccountId = string.Empty;
            Name = string.Empty;
            Roles = Array.Empty<string>();
        }

        public string AccountId { get; set; }



        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}