using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;

namespace BankApp.api.Dtos.AccountDtos
{
    public class TransferAmountDto
    {
        public string reciverAccountId { get; set; }
        public string senderAccountId { get; set; }
        public string currencycode { get; set; }
        public float amount { get; set; }
        public TransactionService transactionservice { get; set; }
        public string senderPassword { get; set; }

    }
}
