using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class ErrorInitializingDB : Exception
    {
        public ErrorInitializingDB() :
            base(String.Format("An Error Occurred while Initializing DB"))

        { }
    }
}
