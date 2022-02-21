using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class DepositAmountDto
    {
        [Required]
        public  string accountId { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public  string currencyCode {get;set;}
        [Required]
        public float amount { get; set; }
    }
}
