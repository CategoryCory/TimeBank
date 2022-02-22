using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobScheduleService
    {
        Task<ApplicationResult> AddJobScheduleRangeAsync(ICollection<JobSchedule> jobSchedules);
        Task<JobSchedule> GetJobScheduleByIdAsync(int jobScheduleId);
        Task<List<JobSchedule>> GetJobSchedulesByJobIdAsync(int jobId);
    }
}