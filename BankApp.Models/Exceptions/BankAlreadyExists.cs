using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class BankAlreadyExists : Exception
    {
        public BankAlreadyExists():
            base(String.Format("The Bank with the given Bank ID has been Registered"))

            {}
    }
}
