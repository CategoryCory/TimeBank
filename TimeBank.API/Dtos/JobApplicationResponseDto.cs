using System;

namespace TimeBank.API.Dtos
{
    public record JobApplicationResponseDto
    {
        public int JobApplicationId { get; init; }
        public string Status { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? ResolvedOn { get; init; }
        public int JobId { get; init; }
        public string JobName { get; init; }
        public string JobCreatedByName { get; init; }
        public string JobCategory { get; init; }
        public UserProfileResponseDto Applicant { get; init; }
        public JobScheduleDto JobApplicationSchedule { get; init; }
    }
}
