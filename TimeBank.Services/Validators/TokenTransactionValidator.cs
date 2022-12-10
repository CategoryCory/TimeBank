using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public sealed class TokenTransactionValidator : AbstractValidator<TokenTransaction>
    {
        public TokenTransactionValidator()
        {
        }
    }
}
