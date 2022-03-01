using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class TransferAmountDto
    {
        [Required]
        public string reciverAccountId { get; set; }
        [Required]
        public string senderAccountId { get; set; }
        [Required]
        public string currencycode { get; set; }
        [Required]
        [RegularExpression(@"^[^-]\d+$",ErrorMessage ="Please eneter Valid amount")]
        public string amount { get; set; }
        [Required]
        public string transactionservice { get; set; }
        [Required]
        public string senderPassword { get; set; }

    }
}
