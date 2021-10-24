using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspire;

namespace AuthAndAccount.Domain
{
    public class Account : IAccount
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}