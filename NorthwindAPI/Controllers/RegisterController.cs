using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPI.ContextService;
using NorthwindAPI.Models;
using NorthwindAPI.Utils;
using NorthwindAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        [HttpPost]
        public string Post([FromBody] UserRegisterDTO userRegisterDTO)
        {
            //check User have existing in database
            if (!(northwindContext.TbIUsers.Any(u => u.Username.Equals(userRegisterDTO.Username)))
                && !(northwindContext.TbIUsers.Any(u => u.CustomerId.Equals(userRegisterDTO.CustomerId))))
            {
                ValidatorResult validatorResult = UserValidator.IsValidUser(userRegisterDTO);
                if (!validatorResult.IsValid)
                {
                    HttpContext.Response.StatusCode = 422;
                    return JsonConvert.SerializeObject(validatorResult.ValidationMessage);
                }

                TbIUser user = new TbIUser();
                user.Username = userRegisterDTO.Username;
                var customer = northwindContext.Customers.Find(userRegisterDTO.CustomerId);
                if (customer == null) {
                    HttpContext.Response.StatusCode = 422;
                    return JsonConvert.SerializeObject("Wrong CustomerId.");
                }
                user.CustomerId = userRegisterDTO.CustomerId;
                user.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(userRegisterDTO.Password),
                    Convert.FromBase64String(user.Salt)));

                //Add to DB
                try
                {
                    northwindContext.Add(user);
                    northwindContext.SaveChanges();
                    HttpContext.Response.StatusCode = 200;
                    return JsonConvert.SerializeObject("Register successfully");
                }
                catch (Exception e)
                {
                    HttpContext.Response.StatusCode = 520;
                    return JsonConvert.SerializeObject(e.Message);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 422;
                return JsonConvert.SerializeObject("User is existing in Database.");
            }
        }
    }
}
