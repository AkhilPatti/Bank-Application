using BankApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BankApp.api.Validators;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class GetTransactionDto
    {
        [Required]
        [MaxLength(90)]
        public string transactionId { get; set; }
        [Required]
        [MaxLength(25)]
        
        public string sourceAccountId { get; set; }
        [Required]
        
        [MaxLength(25)]
        
        public string receiveraccountId { get; set; }

        [Required]
        [AmountValidator(ErrorMessage = "Please eneter correct amount value")]
        public float amount { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public DateTime on { get; set; }
    }
}
