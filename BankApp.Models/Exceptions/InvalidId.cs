using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class InvalidId : Exception
    {
        public InvalidId()
            : base(String.Format("Enter a valid Account Id\n"))
        { }
    }
}
