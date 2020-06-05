using Aspire.Application.AppServices;

using Microsoft.AspNetCore.Mvc;

namespace Aspire.Identity.Identities
{
    public class IdentityAppServices : AppService
    {
        [HttpPost]
        public string SignIn()
        {
            return "SignIn";
        }

        [HttpPost]
        public string SignUp()
        {
            return "SignUp";
        }

        [HttpGet]
        public string SignOut()
        {
            return "SignOut";
        }

        [HttpGet]
        public string ForgetPassword()
        {
            return "ForgetPassword";
        }
    }
}
