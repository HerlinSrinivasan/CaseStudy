using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.Models
{
    public class Resumes
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        [ForeignKey("JobSeeker")]
        public int JobSeekerId { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        public JobSeekers JobSeeker { get; set; } // nav propoerty
    }
}


