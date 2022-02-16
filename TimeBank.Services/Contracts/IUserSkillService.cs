using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IUserSkillService
    {
        Task<ApplicationResult> AddSkillRangeAsync(List<UserSkill> userSkill);
        Task<List<UserSkill>> GetSkillsAsync(string searchString);
    }
}