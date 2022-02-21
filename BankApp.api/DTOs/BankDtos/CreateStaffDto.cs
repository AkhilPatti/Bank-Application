using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.DTOs.BankDtos
{
    public class CreateStaffDto
    {
        
        public string staffName { get; set; }
        
        
        public string password { get; set; }
        
        public string bankId { get; set; }
    }
}
