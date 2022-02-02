using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Dtos.AccountDtos
{
    public class GetBalanceDTO
    {
        public string accountId  { get; set; }
        public string balance { get; set; }
    }
}
