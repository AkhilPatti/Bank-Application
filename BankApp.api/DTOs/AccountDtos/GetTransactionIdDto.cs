using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class GetTransactionIdDto
    {
        [Required]
        private string accountId {get;set;}
        [Required]
        private string transactionId { get; set; }
    }
}
