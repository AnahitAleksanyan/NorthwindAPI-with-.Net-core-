using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPI.ContextService;
using NorthwindAPI.Models;
using NorthwindAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        [HttpPost]
        public string Post([FromBody] CourseDTO courseDTO)
        {
            ValidatorResult validatorResult = CourseValidator.IsValidCourse(courseDTO);
            if (!validatorResult.IsValid)
            {
                HttpContext.Response.StatusCode = 422;
                return JsonConvert.SerializeObject(validatorResult.ValidationMessage);
            }

            Course course = new Course()
            {
                Name = courseDTO.Name,
                Description = courseDTO.Description,
                Length=courseDTO.Length,
                StartDate=courseDTO.StartDate,
                EndDate=courseDTO.EndDate
            };

            //Add to DB
            try
            {
                northwindContext.Add(course);
                northwindContext.SaveChanges();
                HttpContext.Response.StatusCode = 200;
                return JsonConvert.SerializeObject("The course is successfully saved.");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 520;
                return JsonConvert.SerializeObject(e.Message);
            }
        }
    }
}
