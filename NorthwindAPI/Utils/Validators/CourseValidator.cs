using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Validators
{
    public static class CourseValidator
    {
        public static ValidatorResult IsValidCourse(CourseDTO course)
        {
            ValidatorResult validatorResult = new ValidatorResult(true);
            if (course == null)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The course is null.";
                return validatorResult;
            }

            if (course.Name.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The course name is required.";
                return validatorResult;
            }
            if (course.Description.Trim().Equals(""))
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The course description is required.";
                return validatorResult;
            }
            if (course.Length==0)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The course length is required.";
                return validatorResult;
            }
            if (!course.StartDate.HasValue)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user start date is required.";
                return validatorResult;
            }
            if (!course.EndDate.HasValue)
            {
                validatorResult.IsValid = false;
                validatorResult.ValidationMessage = "The user end date is required.";
                return validatorResult;
            }
            return validatorResult;
        }
    }
}
