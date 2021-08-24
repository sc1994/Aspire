using Aspire;

namespace template_1qaz2wsx3edc.Domain.Accounts
{
    public class Account : IAccount
    {
        public string Name { get; set; }
        public string AccountId { get; set; }
        public string[] Roles { get; set; }
    }
}
