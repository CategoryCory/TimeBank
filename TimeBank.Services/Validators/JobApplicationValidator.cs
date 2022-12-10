using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public sealed class JobApplicationValidator : AbstractValidator<JobApplication>
    {
        public JobApplicationValidator()
        {
            RuleFor(r => r.JobId).NotEmpty().WithMessage("You must provide a job for this response.");
            RuleFor(r => r.ApplicantId).NotEmpty().WithMessage("You must provide a user for this response.");
        }
    }
}
