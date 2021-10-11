using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class NotEnoughBalance : Exception
    {
        public NotEnoughBalance()
            : base(String.Format("Your account does't Have enough money to transfer or Withdraw\n"))
        { }
    }
}
