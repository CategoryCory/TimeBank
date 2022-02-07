using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobApplicationService
    {
        Task<ApplicationResult> AddJobApplicationAsync(JobApplication jobResponse);
        Task<ApplicationResult> EditJobApplicationStatusByIdAsync(int id, string newStatus);
        Task<List<JobApplication>> GetAllApplicationsByUserIdAsync(string userId);
        Task<JobApplication> GetApplicationByIdAsync(int id);
    }
}