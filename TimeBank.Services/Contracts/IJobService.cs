using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobService
    {
        Task<ApplicationResult> AddJobAsync(Job job);
        Task<ApplicationResult> DeleteJobAsync(Guid displayId);
        Task<List<Job>> GetAllJobsAsync(string userId = null, bool includeUserData = false);
        Task<Job> GetJobByDisplayIdAsync(Guid displayId, bool includeUserData = false);
        Task<ApplicationResult> UpdateJobAsync(Job job);
    }
}