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
    public class CustomerCourseController : ControllerBase
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        [HttpPost]
        public string Post([FromBody] CourseCustomerCastDTO courseCustomerCastDTO)
        {
            ValidatorResult validatorResult = CourseCustomerValidator.IsValidCourseCustomer(courseCustomerCastDTO);
            if (!validatorResult.IsValid)
            {
                HttpContext.Response.StatusCode = 422;
                return JsonConvert.SerializeObject(validatorResult.ValidationMessage);
            }

            var customer = northwindContext.Customers.Find(courseCustomerCastDTO.CustomerId);
            var course = northwindContext.Courses.Find(courseCustomerCastDTO.CourseId);
            if (customer == null || course == null)
            {
                HttpContext.Response.StatusCode = 422;
                return JsonConvert.SerializeObject("There is no course or customer with that id.");
            }
            var courseCustomer = northwindContext.CourseCustomerCasts.Where(cc => cc.CourseId == courseCustomerCastDTO.CourseId && cc.CustomerId == courseCustomerCastDTO.CustomerId).FirstOrDefault();

            if (courseCustomer != null)
            {
                HttpContext.Response.StatusCode = 422;
                return JsonConvert.SerializeObject("This course customer pair is already exist.");
            }

            CourseCustomerCast courseCustomerCast = new CourseCustomerCast()
            {
                CourseId = courseCustomerCastDTO.CourseId,
                CustomerId = courseCustomerCastDTO.CustomerId
            };

            //Add to DB
            try
            {
                northwindContext.Add(courseCustomerCast);
                northwindContext.SaveChanges();
                HttpContext.Response.StatusCode = 200;
                return JsonConvert.SerializeObject("The courseCustomer is successfully saved.");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 520;
                return JsonConvert.SerializeObject(e.Message);
            }

        }

        [HttpGet("{CustomerId}")]
        public string Get(string CustomerId)
        {
            var customerCourses = northwindContext.CourseCustomerCasts.Where(cc => cc.CustomerId == CustomerId).ToList();
            var courses = new LinkedList<Course>();
            foreach (var item in customerCourses)
            {
                var course = northwindContext.Courses.Where(c => c.CourseId == item.CourseId)
                                                     .FirstOrDefault();
                if (course != null)
                {
                    courses.AddLast(course);
                }
            }
            return JsonConvert.SerializeObject(courses);
        }
    }
}

