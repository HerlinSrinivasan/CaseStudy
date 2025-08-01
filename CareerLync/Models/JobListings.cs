using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CareerLync.Models
{
    public class JobListings
    {
        [Key]
        public int JobListingId { get; set; }

        [Required]
        [ForeignKey("Employer")]
        public int EmployerId { get; set; }

        [Required,MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; } 

        public string Location { get; set; }

        [Required]
        public string SkillsRequired { get; set; }

        public string SalaryRange { get; set; }


        [Required]
        public string Status { get; set; } //avaible, closed

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        //nav properties
        public Employers Employer { get; set; }
        public ICollection<JobApplications> JobApplications { get; set; } //nav property - one job has may applicatiosn
    }
}
