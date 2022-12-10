using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record JobDto
    {
        public Guid DisplayId { get; init; }

        [Required]
        [MaxLength(100, ErrorMessage = "The job name cannot be longer than 100 characters.")]
        public string JobName { get; init; }

        [Required]
        [MaxLength(250, ErrorMessage = "The job description cannot be longer than 250 characters.")]
        public string Description { get; init; }

        [Required]
        public string JobScheduleType { get; init; }

        [Required]
        public DateTime ExpiresOn { get; init; }

        public string JobStatus { get; init; }

        [Required]
        public int JobCategoryId { get; init; }

        public string CreatedById { get; init; }

        public ICollection<JobScheduleDto> JobSchedules { get; init; } = new List<JobScheduleDto>();
    }
}
