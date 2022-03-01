using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.DTOs.BankDtos
{
    public class CreateStaffDto
    {
        [Required]
        [RegularExpression("^[A-Za-Z]+$", ErrorMessage = "Please enter a valid user name")]
        public string staffName { get; set; }
        
        [Required]
        public string password { get; set; }
        [Required]
        public string bankId { get; set; }
    }
}
