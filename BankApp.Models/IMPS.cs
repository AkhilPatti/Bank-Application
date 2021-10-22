using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    class IMPS : Transaction
    {
        private int sameAccountCharge = 5; //charges for transcation between same BankAccounts
        private int otherAccountCharge = 6; //charges for transcation between same BankAccounts
        public int minLimit = 2; 
        public int maxLimit = 400000;

    }
}
