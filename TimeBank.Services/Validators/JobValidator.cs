using FluentValidation;
using TimeBank.Entities.Models;

namespace TimeBank.Services.Validators
{
    public class JobValidator : AbstractValidator<Job>
    {
        public JobValidator()
        {
            RuleFor(j => j.DisplayId)
                .NotEmpty()
                .WithMessage("A unique identifier must be provided for the display ID.");
            RuleFor(j => j.JobName)
                .NotEmpty()
                .WithMessage("You must enter a name for this job.")
                .MaximumLength(100)
                .WithMessage("The job name must not exceed 100 characters.");
            RuleFor(j => j.Description)
                .NotEmpty()
                .WithMessage("You must provide a description for this job.")
                .MaximumLength(250)
                .WithMessage("The description must not exceed 250 characters.");
            RuleFor(j => j.ExpiresOn)
                .NotEmpty()
                .WithMessage("You must provide an expiration date for this job.");
        }
    }
}
