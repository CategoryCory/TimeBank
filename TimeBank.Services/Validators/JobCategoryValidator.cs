using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public sealed class JobCategoryValidator : AbstractValidator<JobCategory>
    {
        public JobCategoryValidator()
        {
            RuleFor(c => c.JobCategoryName)
                .NotEmpty()
                .WithMessage("You must enter a job category name.")
                .MaximumLength(150)
                .WithMessage("The category name cannot exceed 150 characters.");
        }
    }
}
