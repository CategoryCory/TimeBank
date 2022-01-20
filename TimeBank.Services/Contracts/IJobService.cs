using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobService
    {
        Task<ApplicationResult> CreateNewJobAsync(Job job);
        Task<ApplicationResult> DeleteJobAsync(Guid displayId);
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByDisplayIdAsync(Guid displayId);
        Task<Job> GetJobByIdAsync(int id);
        Task<ApplicationResult> UpdateJobAsync(Job job);
    }
}