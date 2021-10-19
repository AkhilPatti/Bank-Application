using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class InvalidBankId : Exception
    {
        public InvalidBankId()
            : base(String.Format("Please enter a Valid BankID"))
        { }
    }
}
