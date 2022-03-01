using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BankApp.api.Validators;
using Newtonsoft.Json;

namespace BankApp.api.Dtos.AccountDtos
{
    public class DepositAmountDto
    {
        [Required]
        public string accountId { get; set; }
        [Required]
        public string password
        {
            get;
            set;
        }
        [Required]
        public string currencyCode { get; set; }
        [Required]
        public string amount
        {
            get;
            set;
        }
    }

}