using System.Threading;
using Aspire;

namespace template_1qaz2wsx3edc.AppService.Accounts
{
    public class AccountAppService : AppServiceBase
    {
        public object Add(string aaa)
        {
            Thread.Sleep(3000);
            return new
            {
                aaa
            };
        }
    }
}