using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankApp.api.Dtos.AccountDtos
{
    public class LoginDto
    {
        [Required]
        public string accountId { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
    }
}
