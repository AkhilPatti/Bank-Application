using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.api.Validators
{

    public class AmountValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.GetType() == typeof(string))
            {
                return false;
            }
            return true;

        }
    }
    
}
