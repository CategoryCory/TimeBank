﻿using System;

namespace TimeBank.API.Dtos
{
    public class JobApplicationResponseDto
    {
        public int JobApplicationId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ResolvedOn { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string JobCreatedByName { get; set; }
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
    }
}
