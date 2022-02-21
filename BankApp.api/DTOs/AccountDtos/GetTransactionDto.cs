using BankApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class GetTransactionDto
    {
        [MaxLength(90)]
        public string tranactionId { get; set; }
        [Required]
        [MaxLength(25)]
        
        public string sourceAccountId { get; set; }
        [Required]
        
        [MaxLength(25)]
        
        public string receiveraccountId { get; set; }

        [Required]
        public float amount { get; set; }

        [Required]
        public TransactionType type { get; set; }

        [Required]
        public DateTime on { get; set; }
    }
}
