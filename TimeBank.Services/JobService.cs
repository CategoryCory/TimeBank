using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Entities.Models;
using TimeBank.Repository;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobService> _logger;
        private readonly JobValidator _jobValidator;

        public JobService(ApplicationDbContext context, ILogger<JobService> logger)
        {
            _context = context;
            _logger = logger;
            _jobValidator = new JobValidator();
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.AsNoTracking().ToListAsync();
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Job> GetJobByDisplayIdAsync(Guid displayId)
        {
            return await _context.Jobs.FirstOrDefaultAsync(j => j.DisplayId == displayId);
        }

        public async Task<ApplicationResult> CreateNewJobAsync(Job job)
        {
            job.DisplayId = Guid.NewGuid();

            ValidationResult result = _jobValidator.Validate(job);

            if (!result.IsValid)
            {
                _logger.LogError($"Failed to create new job with name {job.JobName}");

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> UpdateJobAsync(Job job)
        {
            var jobToUpdate = await _context.Jobs.FirstOrDefaultAsync(j => j.DisplayId == job.DisplayId);

            if (jobToUpdate is null)
            {
                return ApplicationResult.Failure(new List<string> { $"The job with name {job.JobName} could not be found." });
            }

            job.JobId = jobToUpdate.JobId;
            ValidationResult result = _jobValidator.Validate(job);

            if (!result.IsValid)
            {
                _logger.LogError($"Failed to update job with name {job.JobName}");

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> DeleteJobAsync(Guid displayId)
        {
            var jobToDelete = await _context.Jobs.FirstOrDefaultAsync(j => j.DisplayId == displayId);

            if (jobToDelete is null)
            {
                return ApplicationResult.Failure(new List<string> { $"The job with display id {displayId} could not be found." });
            }

            _context.Jobs.Remove(jobToDelete);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
