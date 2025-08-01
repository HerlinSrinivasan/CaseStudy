
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.Models
{
    public class Employers
    {
        [Key]
        public int EmployerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required,MaxLength(150)]
        public string CompanyName { get; set; }

        [Url,MaxLength(300)]
        public string CompanyWebsite { get; set; }

        public bool IsDeleted { get; set; } = false; //soft delete flag

        public Users User { get; set; } // nav to user (one to one)

        public ICollection<JobListings> JobListings { get; set; } // one-to-many R ;
    }
}
