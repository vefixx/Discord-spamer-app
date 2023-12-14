using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Roots
{
    public class Account
    {
        public string username { get; set; }
        public string token { get; set; }
    }

    public class Root_accounts
    {
        public List<Account> accounts { get; set; }
    }
}
