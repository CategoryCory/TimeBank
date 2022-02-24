using System;
using System.Collections.Generic;
using TimeBank.Repository.Models;

namespace TimeBank.API.Dtos
{
    public class JobResponseDto
    {
        public Guid DisplayId { get; set; }
        public string JobName { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string JobStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public int JobCategoryId { get; set; }
        public string JobCategory { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public string JobScheduleType { get; set; }
        public ICollection<JobScheduleDto> JobSchedules { get; set; } = new List<JobScheduleDto>();
    }
}
