
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CareerLync.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required,MinLength(6)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } //employer or jobseeker

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false; //soft delete flag

        //one-to-one R with E and JS
        public JobSeekers JobSeekers { get; set; }

        public Employers Employers { get; set; }
    }
}
