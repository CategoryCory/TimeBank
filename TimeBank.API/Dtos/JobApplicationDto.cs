using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobApplicationDto
    {
        [Required(ErrorMessage = "The job id is missing.")]
        public int JobId { get; set; }

        public string ApplicantId { get; set; }

        public int JobApplicationScheduleId { get; set; }
    }
}
