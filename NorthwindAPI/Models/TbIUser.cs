using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    public class TbIUser
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }

        public string CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public TbIUser(string username, string password, string salt)
        {
            Username = username;
            Password = password;
            Salt = salt;
        }

        public TbIUser()
        {
        }
    }
}
