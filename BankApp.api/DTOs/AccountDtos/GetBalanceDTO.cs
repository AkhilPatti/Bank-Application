using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BankApp.api.Dtos.AccountDtos
{
    public class GetBalanceDTO
    {
        [Required]
        public string accountId  { get; set; }
        [Required]
        public string balance { get; set; }
    }
}
