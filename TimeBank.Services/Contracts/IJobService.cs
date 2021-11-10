using TimeBank.Entities.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobService
    {
        Task CreateNewJobAsync(Job job);
        Task DeleteJobAsync(Job job);
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByDisplayIdAsync(Guid displayId);
        Task<Job> GetJobByIdAsync(int id);
        Task UpdateJobAsync(Job job);
    }
}