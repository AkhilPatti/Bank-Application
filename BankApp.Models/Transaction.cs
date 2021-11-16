using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public string tranactionId { get; set; }
        public string sourceAccountId { get; set; }
        public string accountId { get; set; }
        public float amount { get; set; }
        public TransactionType type {get; set;}
        public DateTime on { get; set; }
        public string sourceBankId { get; set; }
        public string recieverBankId { get; set; }
        
    }
}
