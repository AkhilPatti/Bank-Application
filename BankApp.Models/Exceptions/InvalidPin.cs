using System;
using System.Collections.Generic;


namespace BankApp.Models.Exceptions
{
    public class InvalidPin : Exception
    {

        public InvalidPin()
            : base(String.Format("Incorrect Pin\n"))
        { }
    }
}
