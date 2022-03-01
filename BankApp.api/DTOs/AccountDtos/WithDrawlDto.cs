using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class WithDrawlDto
    {
        
        [Required]
        [RegularExpression(@"^[^-]\d+$", ErrorMessage = "Please eneter Valid amount")]
        public string amount {get ;set;}
        [Required]
        public string accountId { get; set; }
        [Required]
        public string password { get; set; }
    }
}
