using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(m => m.Body).MaximumLength(500).NotEmpty();
        }
    }
}
