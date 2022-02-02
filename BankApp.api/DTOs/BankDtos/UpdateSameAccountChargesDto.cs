using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.DTOs.BankDtos
{
    public class UpdateSameAccountChargesDto
    {
        public float newSameAccountImpsCharge { get; set; }
        public float newSameAccountRtgsCharge { get; set; } 
    }
}
