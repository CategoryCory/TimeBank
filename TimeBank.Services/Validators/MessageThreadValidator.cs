using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class MessageThreadValidator : AbstractValidator<MessageThread>
    {
        public MessageThreadValidator()
        {
            RuleFor(t => t.JobId).NotEmpty();
            RuleFor(t => t.FromUserId).NotEmpty();
            RuleFor(t => t.ToUserId).NotEmpty();
        }
    }
}
