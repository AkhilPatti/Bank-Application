using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class RTGS : Transaction
    {
        public int minLimit = 400000;
        public float sameAccountCharge = 0; //charges for transcation between same BankAccounts
        public float otherAccountcharge = 2; //charges for transcation between different BankAccounts
    }
}
