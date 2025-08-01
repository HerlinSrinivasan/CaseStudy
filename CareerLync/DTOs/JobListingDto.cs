namespace CareerLync.DTOs
{
    public class JobListingDto
    {
        public int JobListingId { get; set; }
        public int EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
