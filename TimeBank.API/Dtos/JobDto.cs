using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The job name cannot be longer than 100 characters.")]
        public string JobName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "The job description cannot be longer than 250 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        [Required]
        public string JobStatus { get; set; }

        [Required]
        public int JobCategoryId { get; set; }

        [Required]
        public string CreatedById { get; set; }
    }
}
