using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public string TranactionId { get; set; }
        public string SourceAccountId { get; set; }
        public string AccountId { get; set; }
        public int Amount { get; set; }
        public TransactionType Type {get; set;}
        public DateTime On { get; set; }
        public string bankId { get; set; }
        public Tuple<string,string,TransactionType,DateTime,int> getDetails()
        { return Tuple.Create(SourceAccountId, AccountId, Type, On, Amount); }
    }
}
