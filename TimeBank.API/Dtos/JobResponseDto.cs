using System;
using System.Collections.Generic;

namespace TimeBank.API.Dtos
{
    public record JobResponseDto
    {
        public int JobId { get; init; }
        public string DisplayId { get; set; }
        public string JobName { get; init; }
        public string Description { get; init; }
        public DateTime ExpiresOn { get; init; }
        public string JobStatus { get; init; }
        public DateTime CreatedOn { get; init; }
        public int JobCategoryId { get; init; }
        public string JobCategory { get; init; }
        public string CreatedById { get; init; }
        public string CreatedBy { get; init; }
        public string JobScheduleType { get; init; }
        public ICollection<JobScheduleDto> JobSchedules { get; init; } = new List<JobScheduleDto>();
        public ICollection<JobApplicationResponseDto> JobApplications { get; init; } = new List<JobApplicationResponseDto>();
    }
}
