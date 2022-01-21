using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class TokenTransactionValidator : AbstractValidator<TokenTransaction>
    {
        public TokenTransactionValidator()
        {
        }
    }
}
