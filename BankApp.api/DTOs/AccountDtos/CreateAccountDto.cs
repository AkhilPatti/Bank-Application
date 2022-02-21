using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class CreateAccountDto
    {
            [Required]
            public string accountHolderName { get; set; }
            
            [Required]
            public string phoneNumber { get; set; }

            [Required]           
            public string bankId { get; set; }
            [Required]
            public string password { get; set; }
        
    }
}
