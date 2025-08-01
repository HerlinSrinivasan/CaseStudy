using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.DTOs
{
    public class JobApplicationDto
    {
        public int ApplicationId { get; set; }
        public int JobListingId { get; set; }

        public int JobSeekerId { get; set; }

        public int ResumeId { get; set; }

        public string Status { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
