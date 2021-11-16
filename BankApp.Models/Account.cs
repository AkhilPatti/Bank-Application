using System;
using System.Collections.Generic;
namespace BankApp.Models
{
    public class Account
    {
        public string accountId { get; set; }
        public string accountHolderName { get; set; }
        public string pin { get; set; }
        public string phoneNumber { get; set; }
        public float balance { get; set; }
        public List<Transaction> transactions { get; set; }
       

    }
}
