using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IUserRatingService
    {
        Task<ApplicationResult> AddRatingAsync(UserRating userRating);
        Task<List<UserRating>> GetAllReceivedRatingsByUserIdAsync(string userId);
        Task<double> GetAverageRatingByUserIdAsync(string userId);
    }
}