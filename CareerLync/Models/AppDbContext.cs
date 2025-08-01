using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
namespace CareerLync.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<JobListings>().Property(j => j.SalaryRange).HasPrecision(18, 2);

            //job app -> joblist (mant to one)
            modelBuilder.Entity<JobApplications>()
                .HasOne(app => app.JobListing)
                .WithMany(job => job.JobApplications)
                .HasForeignKey(app => app.JobListingId).OnDelete(DeleteBehavior.Restrict);

            //jobApp -> JobSeeker ( many to one)

            modelBuilder.Entity<JobApplications>()
                 .HasOne(app => app.JobSeeker)
                 .WithMany(js => js.Application)
                 .HasForeignKey(app => app.JobSeekerId)
                 .OnDelete(DeleteBehavior.Restrict);

            //resume -> jobseeker ( one to one)

            modelBuilder.Entity<Resumes>()
                .HasOne(r => r.JobSeeker)
                .WithMany(js => js.Resume)
                .HasForeignKey(r => r.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<JobSeekers>().HasQueryFilter(js => !js.isDeleted);
            modelBuilder.Entity<Employers>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<JobListings>().HasQueryFilter(j => !j.IsDeleted);
            modelBuilder.Entity<JobApplications>().HasQueryFilter(a => !a.IsDeleted);
                 modelBuilder.Entity<Resumes>().HasQueryFilter( r => !r.IsDeleted);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<JobSeekers> JobSeekers { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<JobListings> JobListings { get; set; }
        public DbSet<JobApplications> JobApplications { get; set; }
        public DbSet<Resumes> Resumes { get; set; }
    }
}

