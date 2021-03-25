using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NorthwindAPI.Models
{
    public class Course
    {
        [Key]
        public virtual int CourseId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Length { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<CourseCustomerCast> CourseCustomerCasts { get; set; }
    }
}
