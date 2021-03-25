using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Validators
{
    public static class CourseCustomerValidator
    {
        public static ValidatorResult IsValidCourseCustomer(CourseCustomerCastDTO courseCustomerCast)
        {
            ValidatorResult validatorResult = new ValidatorResult(true);
            if (courseCustomerCast == null)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The courseCustomerCast is null.";
                return validatorResult;
            }

            if (courseCustomerCast.CourseId==0)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The courseId is required.";
                return validatorResult;
            }
            if (courseCustomerCast.CustomerId.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The customerId is required.";
                return validatorResult;
            }
            return validatorResult;
        }
    }
}
