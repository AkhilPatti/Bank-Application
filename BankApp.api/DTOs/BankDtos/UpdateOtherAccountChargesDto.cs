using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BankApp.api.Validators;

namespace BankApp.api.DTOs.BankDtos
{
    public class UpdateOtherAccountChargesDto
    {
        [AmountValidator(ErrorMessage = "Please eneter correct  value")]
        [Range(0, 1, ErrorMessage = "Please enter the charges in range of 0.00 to 1.00")]
        
        public float newOtherAccountImpsCharge { get; set; }
        [Range(0, 1, ErrorMessage = "Please enter the charges in range of 0.00 to 1.00")]
        
        [AmountValidator(ErrorMessage = "Please eneter correct amount value")]
        public float newOtherAccountRtgsCharge { get; set; }
    }
}
