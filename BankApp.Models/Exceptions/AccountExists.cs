using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class AccountExists : Exception
    {
        public AccountExists()
            : base(String.Format("The Account with this Name already Exists"))
        { }
    }
}
