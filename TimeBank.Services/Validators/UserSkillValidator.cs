using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class UserSkillValidator : AbstractValidator<UserSkill>
    {
        public UserSkillValidator()
        {
            RuleFor(s => s.SkillName)
                .NotEmpty()
                .WithMessage("You must enter a skill name.")
                .MaximumLength(50)
                .WithMessage("The skill name cannot exceed 50 characters.");
        }
    }
}
