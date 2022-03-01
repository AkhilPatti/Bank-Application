using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class DisplayAccountDto
    {
        [Required]
        public string accountId { get; set; }
        [RegularExpression("^[A-Za-Z]+$", ErrorMessage = "Please enter a valid user name")]
        [Required]
        public string accountHolderName { get; set; }
        [RegularExpression("^[0-9]{10}$",ErrorMessage ="Please enter a Valid Phone Number")]
        [Required]
        public string phoneNumber { get; set; }
        [Range(0,1000000,ErrorMessage ="Please Enter a Valid Balance")]
        [Required]
        public float balance { get; set; }
        [Required]
        public string bankId { get; set; }
    }
}
