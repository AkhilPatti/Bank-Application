using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class DisplayAccountDto
    {
        public string accountId { get; set; }
        
        public string accountHolderName { get; set; }
        
        public string phoneNumber { get; set; }
        
        public float balance { get; set; }
        
        public string bankId { get; set; }
    }
}
