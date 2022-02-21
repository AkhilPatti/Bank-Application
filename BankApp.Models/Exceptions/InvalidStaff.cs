using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models.Exceptions
{
    public class InvalidStaff : Exception
    {
        public InvalidStaff() :
            base(String.Format("please enter a valid staff Id that belongs to this bank"))
        {}
    }
}
