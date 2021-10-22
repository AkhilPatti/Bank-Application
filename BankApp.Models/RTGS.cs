using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    class RTGS : Transaction
    {
        public int minLimit = 400000;
        private int sameAccountCharge = 0; //charges for transcation between same BankAccounts
        private int otherAccountcharge = 2; //charges for transcation between different BankAccounts
    }
}
