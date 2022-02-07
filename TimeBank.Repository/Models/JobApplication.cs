using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public enum JobApplicationStatus
    {
        Pending,
        Accepted,
        Declined,
        Completed
    }

    public class JobApplication
    {
        public int JobApplicationId { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ResolvedOn { get; set; }

        public Guid JobDisplayId { get; set; }
        public Job Job { get; set; }

        public string ApplicantId { get; set; }
        public ApplicationUser Applicant { get; set; }
    }
}
