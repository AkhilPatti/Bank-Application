using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.DTOs.BankDtos
{
    public class UpdateOtherAccountChargesDto
    {
        public float newOtherAccountImpsCharge { get; set; }
        public float newOtherAccountRtgsCharge { get; set; }
    }
}
