using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobDto
    {
        public Guid DisplayId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The job name cannot be longer than 100 characters.")]
        public string JobName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "The job description cannot be longer than 250 characters.")]
        public string Description { get; set; }

        [Required]
        public string JobScheduleType { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        public string JobStatus { get; set; }

        [Required]
        public int JobCategoryId { get; set; }

        public string CreatedById { get; set; }

        public ICollection<JobScheduleDto> JobSchedules { get; set; } = new List<JobScheduleDto>();
    }
}
