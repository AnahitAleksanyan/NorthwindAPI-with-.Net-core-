using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    public class CourseCustomerCast
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Course Course { get; set; }
    }
}
