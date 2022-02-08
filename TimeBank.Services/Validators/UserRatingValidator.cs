using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class UserRatingValidator : AbstractValidator<UserRating>
    {
        public UserRatingValidator()
        {
            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(1)
                .WithMessage("The user rating must be between 1 and 5.")
                .LessThanOrEqualTo(5)
                .WithMessage("The user rating must be between 1 and 5.");
            RuleFor(r => r.Comments)
                .MaximumLength(200)
                .WithMessage("The comment must not be longer than 200 characters.");
            RuleFor(r => r.AuthorId)
                .NotEmpty()
                .WithMessage("The author ID field must not be empty.");
            RuleFor(r => r.RevieweeId)
                .NotEmpty()
                .WithMessage("The reviewee ID field must not be empty.");
        }
    }
}
