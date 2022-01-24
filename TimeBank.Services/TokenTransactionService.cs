using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class TokenTransactionService : ITokenTransactionService
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

        public async Task<ApplicationResult> AddNewTransactionAsync(TokenTransaction tokenTransaction)
        {
            using var dbTransaction = _context.Database.BeginTransaction();

            try
            {
                // First, add token transaction to database
                _context.TokenTransactions.Add(tokenTransaction);
                await _context.SaveChangesAsync();

                // Second, update balances
                var senderBalance = await _context.TokenBalances.SingleOrDefaultAsync(t => t.UserId == tokenTransaction.SenderId);
                var recipientBalance = await _context.TokenBalances.SingleOrDefaultAsync(t => t.UserId == tokenTransaction.RecipientId);

                if (senderBalance is null || recipientBalance is null)
                {
                    _logger.LogError("The balance could not be loaded.");
                    throw new Exception("The balance could not be loaded.");
                }

                senderBalance.CurrentBalance -= tokenTransaction.Amount;
                recipientBalance.CurrentBalance += tokenTransaction.Amount;
                await _context.SaveChangesAsync();

                // If all went well, commit to database
                dbTransaction.Commit();

                return ApplicationResult.Success();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("There was an error adding this transaction: {message}", ex?.InnerException?.Message);

                return ApplicationResult.Failure(new List<string>
                    {
                        $"There was an error adding this transaction: {ex?.InnerException?.Message}"
                    }
                );
            }
        }

        public async Task<List<TokenTransaction>> GetAllTransactionsByUserIdAsync(string userId)
        {
            List<TokenTransaction> allTransactions = new();

            try
            {
                allTransactions = await _context.TokenTransactions
                    .Where(t => t.SenderId == userId || t.RecipientId == userId)
                    .Include(t => t.Sender)
                    .Include(t => t.Recipient)
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("There was an error retrieving transactions for user {userId}: {message}", userId, ex?.InnerException?.Message);
            }

            return allTransactions;
        }
    }
}
