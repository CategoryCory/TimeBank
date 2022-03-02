using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobApplicationDto
    {
        [Required(ErrorMessage = "The job id is missing.")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "The applicant id is missing.")]
        public string ApplicantId { get; set; }
    }
}
