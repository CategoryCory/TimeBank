using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationResult> SetUserSkillsAsync(ApplicationUser user, List<UserSkill> skillsToSet);
    }
}