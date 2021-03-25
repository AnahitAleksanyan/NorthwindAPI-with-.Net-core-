using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    //Data transfer object for user register.
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CustomerId { get; set; }

    }
}
