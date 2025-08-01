using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.Models
{
    public class JobApplications
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("JobListing")]
        public int JobListingId { get; set; }

        [ForeignKey("JobSeeker")]
        public int JobSeekerId { get; set; }

        [ForeignKey("Resume")]
        public int ResumeId { get; set; }

        [Required]
        public string Status { get; set; } = "Applied";

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;//ADDED FOR SOFT DELETE
        //nav properties
        public JobListings JobListing { get; set; }
        public JobSeekers JobSeeker { get; set; }
        public Resumes Resume { get; set; }
    }
}
