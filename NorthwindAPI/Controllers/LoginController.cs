using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPI.ContextService;
using NorthwindAPI.Models;
using NorthwindAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        [HttpPost]
        public string Post([FromBody] UserLoginDTO userLoginDTO)
        {
            //Check existing
            if (northwindContext.TbIUsers.Any(user => user.Username.Equals(userLoginDTO.Username)))
            {
                TbIUser user = northwindContext.TbIUsers.Where(user => user.Username.Equals(userLoginDTO.Username)).First();

                //Calculate hash password from data of Client and compare with hash in server with salt.
                var client_post_hash_password = Convert.ToBase64String(
                    Common.SaltHashPassword(
                        Encoding.ASCII.GetBytes(userLoginDTO.Password),
                        Convert.FromBase64String(user.Salt)));

                if (client_post_hash_password.Equals(user.Password))
                {
                    return JsonConvert.SerializeObject(user);
                }
                else
                {
                    HttpContext.Response.StatusCode = 412;
                    return JsonConvert.SerializeObject("Wrong Password");
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 401;
                return JsonConvert.SerializeObject("User is not existing in Database");
            }
        }
    }
}
