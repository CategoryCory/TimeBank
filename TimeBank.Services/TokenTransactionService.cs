using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class TokenTransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TokenTransactionService> _logger;
        private readonly TokenTransactionValidator _ttValidator;

        public TokenTransactionService(ApplicationDbContext context, ILogger<TokenTransactionService> logger)
        {
            _context = context;
            _logger = logger;
            _ttValidator = new TokenTransactionValidator();
        }
    }
}
