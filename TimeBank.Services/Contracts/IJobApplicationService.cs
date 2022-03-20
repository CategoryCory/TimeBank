using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobApplicationService
    {
        Task<List<JobApplication>> GetJobApplicationsAsync(string userId);
        Task<List<JobApplication>> GetJobApplicationsByJobAsync(int jobId);
        Task<ApplicationResult> AddJobApplicationAsync(JobApplication jobResponse, ICollection<int> jobAppScheduleIds);
        Task<ApplicationResult> EditJobApplicationStatusByIdAsync(int id, string newStatus);
        //Task<JobApplication> GetApplicationByJobAndUserAsync(string userId, int jobId);
        Task<DateTime?> CheckApplicationDateByJobAndUserAsync(string userId, int jobId);
    }
}