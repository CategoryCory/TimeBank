﻿using System;

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
        public Guid DisplayId { get; set; }
        public string JobName { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresOn { get; set; }
        public JobStatus JobStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
