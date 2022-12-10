using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobService
    {
        Task<ApplicationResult> AddJobAsync(Job job);
        Task<ApplicationResult> DeleteJobAsync(int jobId);
        Task<List<Job>> GetAllJobsAsync(int page, int perPage, string userId = null, bool includeUserData = false, bool includeClosed = false);
        Task<Job> GetJobByIdAsync(int id, bool includeUserData = false);
        Task<ApplicationResult> UpdateJobAsync(Job job);
    }
}