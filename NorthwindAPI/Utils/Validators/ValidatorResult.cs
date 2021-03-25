using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Validators
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }

        public ValidatorResult(bool isValid, string validationMessage)
        {
            IsValid = isValid;
            ValidationMessage = validationMessage;
        }

        public ValidatorResult()
        {
        }
        public ValidatorResult(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
