using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface ITokenBalanceService
    {
        Task<ApplicationResult> AddToBalance(string userId, double amountToAdd);
        Task<ApplicationResult> CreateNewBalance(string userId);
        Task<TokenBalance> GetBalanceByUserId(string userId);
        Task<ApplicationResult> RemoveFromBalance(string userId, double amountToRemove);
    }
}