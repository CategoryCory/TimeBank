using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators;

public class PhotoValidator : AbstractValidator<Photo>
{
    public PhotoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(p => p.URL)
            .MaximumLength(450);
        RuleFor(p => p.UserId)
            .NotEmpty();
    }
}
