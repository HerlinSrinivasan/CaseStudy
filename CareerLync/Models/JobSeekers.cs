using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CareerLync.Models
{
    public class JobSeekers
    {
        [Key]
        public int JobSeekerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string Summary { get; set; }

        public string Skills { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }

        public bool isDeleted { get; set; } = false; //soft delete flag

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Users User { get; set; } // nav to user (one to one)

        //one - many Rs
        public ICollection<Resumes> Resume { get; set; }
        public ICollection<JobApplications> Application { get; set; }
    }
}
