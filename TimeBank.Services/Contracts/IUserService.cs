using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsersAsync(bool showOnlyUnapproved = false);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationResult> UpdateUserAsync(ApplicationUser userToUpdate);
    }
}