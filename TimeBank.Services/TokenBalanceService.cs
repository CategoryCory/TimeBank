using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.Services
{
    public sealed class TokenBalanceService : ITokenBalanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TokenBalanceService> _logger;

        public TokenBalanceService(ApplicationDbContext context, ILogger<TokenBalanceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TokenBalance> GetBalanceByUserId(string userId)
        {
            TokenBalance balance = await _context.TokenBalances.SingleOrDefaultAsync(b => b.UserId == userId);
            return balance;
        }

        public async Task<ApplicationResult> CreateNewBalance(string userId)
        {
            TokenBalance balanceToAdd = new();
            balanceToAdd.UserId = userId;

            _context.TokenBalances.Add(balanceToAdd);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> AddToBalance(string userId, double amountToAdd)
        {
            TokenBalance balance = await _context.TokenBalances.SingleOrDefaultAsync(b => b.UserId == userId);

            if (balance is null)
            {
                _logger.LogError("The balance could not be added to user {user}", userId);

                return ApplicationResult.Failure(new List<string> { $"The balance could not be added to user {userId}." });
            }

            balance.CurrentBalance += amountToAdd;
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> RemoveFromBalance(string userId, double amountToRemove)
        {
            TokenBalance balance = await _context.TokenBalances.SingleOrDefaultAsync(b => b.UserId == userId);

            if (balance is null)
            {
                _logger.LogError("The balance could not be removed from user {user}", userId);

                return ApplicationResult.Failure(new List<string> { $"The balance could not be removed from user {userId}." });
            }

            balance.CurrentBalance -= amountToRemove;
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
