using System;
using System.Collections.Generic;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public enum JobStatus
    {
        Available,
        Unavailable,
        Completed,
        Deleted
    }

    public enum JobScheduleType
    {
        Open,
        Custom
    }

    public class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresOn { get; set; }
        public JobScheduleType JobScheduleType { get; set; }
        public JobStatus JobStatus { get; set; }
        public DateTime CreatedOn { get; set; }

        public int JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }

        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<JobSchedule> JobSchedules { get; set; }
    }
}
