using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Extensions;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobCategoryService> _logger;
        private readonly JobCategoryValidator _jobCategoryValidator;

        public JobCategoryService(ApplicationDbContext context, ILogger<JobCategoryService> logger)
        {
            _context = context;
            _logger = logger;
            _jobCategoryValidator = new JobCategoryValidator();
        }

        public async Task<List<JobCategory>> GetAllCategoriesAsync()
        {
            return await _context.JobCategories.AsNoTracking().ToListAsync();
        }

        public async Task<JobCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.JobCategories.FindAsync(id);
        }

        public async Task<ApplicationResult> AddJobCategoryAsync(JobCategory category)
        {
            ValidationResult result = _jobCategoryValidator.Validate(category);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to create new job category");

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            category.JobCategorySlug = category.JobCategoryName.Slugify();
            _context.JobCategories.Add(category);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
