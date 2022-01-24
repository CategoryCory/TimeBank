using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface ITokenTransactionService
    {
        Task<ApplicationResult> AddNewTransactionAsync(TokenTransaction tokenTransaction);
        Task<List<TokenTransaction>> GetAllTransactionsByUserIdAsync(string userId);
    }
}