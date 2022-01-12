using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class InvalidTransactionId : Exception
    {
        public InvalidTransactionId()
            : base(String.Format("Enter a valid TransactionId\n"))
        { }
    }
}
