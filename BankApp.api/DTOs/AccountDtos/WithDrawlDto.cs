using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class WithDrawlDto
    {
        public float amount {get;set;}
        public string accountId { get; set; }
        public string password { get; set; }
    }
}
