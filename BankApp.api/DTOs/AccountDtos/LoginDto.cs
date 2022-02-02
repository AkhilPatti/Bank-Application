using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class LoginDto
    {
        public string accountId { get; set; } =string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
