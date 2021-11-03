using System;

namespace TimeBank.Entities.Models
{
    public enum JobStatus
    {
        Available,
        Unavailable,
        Completed,
        Deleted
    }

    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresOn { get; set; }
        public JobStatus JobStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
