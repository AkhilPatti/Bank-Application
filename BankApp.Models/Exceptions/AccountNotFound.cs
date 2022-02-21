using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class AccountNotFound : Exception
    {
        public AccountNotFound() :
            base(String.Format("Account with this given Account ID is not present in this bank"))
        { }
    }
}
