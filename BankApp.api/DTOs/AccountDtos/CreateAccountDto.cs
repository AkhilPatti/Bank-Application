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
            [RegularExpression("^[A-Za-z]+$" , ErrorMessage ="Please enter a valid user name")]

            public string accountHolderName { get; set; }
            
            [Required]
            [RegularExpression("^[0-9]{10}$",ErrorMessage = "Enter a Valid Phone number")]
            public string phoneNumber { get; set; }

            [Required]           
            public string bankId { get; set; }
            [Required]
            [RegularExpression(@"^\S+$",ErrorMessage="Password must not contain spaces")]
            public string password { get; set; }
        
    }
}
