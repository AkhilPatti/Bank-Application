using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class GetTransactionIdDto
    {
        private string accountId {get;set;}
        private string transactionId { get; set; }
    }
}
