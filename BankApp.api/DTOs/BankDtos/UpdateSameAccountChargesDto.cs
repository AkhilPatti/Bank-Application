using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BankApp.api.DTOs.BankDtos
{
    public class UpdateSameAccountChargesDto
    {
        [Range(0,1,ErrorMessage="Please enter the charges in range of 0.00 to 1.00")]
        public float newSameAccountImpsCharge { get; set; }
        [Range(0, 1, ErrorMessage = "Please enter the charges in range of 0.00 to 1.00")]
        
        public float newSameAccountRtgsCharge { get; set; }
    }
}
