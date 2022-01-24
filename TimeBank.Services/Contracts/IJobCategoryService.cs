using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IJobCategoryService
    {
        Task<ApplicationResult> AddJobCategoryAsync(JobCategory category);
        Task<List<JobCategory>> GetAllCategoriesAsync();
        Task<JobCategory> GetCategoryByIdAsync(int id);
    }
}