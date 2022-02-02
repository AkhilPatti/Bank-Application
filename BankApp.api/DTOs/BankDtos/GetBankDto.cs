using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.DTOs.BankDtos
{
    public class GetBankDto
    {
        public string bankId { get; set; }
        
        public string bankName { get; set; }
        
        public double sImps { get; set; }

        public double oImps { get; set; }
        
        public double sRtgs { get; set; }
        
        public double oRtgs { get; set; }
    }
}
