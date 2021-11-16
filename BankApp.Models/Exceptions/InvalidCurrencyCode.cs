using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class InvalidCurrencyCode : Exception
    {
        public InvalidCurrencyCode() :
            base(String.Format("Please Choose a Valid CurrecyCode"))
        { }
    }
}
