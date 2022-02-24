﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
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

        public async Task<List<Job>> GetAllJobsAsync(string userId = null, bool includeUserData = false)
        {
            var jobs = _context.Jobs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(userId))
                jobs = jobs.Where(j => j.CreatedById == userId);

            if (includeUserData)
                jobs = jobs.Include(j => j.CreatedBy);

            return await jobs.Include(j => j.JobCategory)
                             .Include(j => j.JobSchedules)
                             .OrderByDescending(j => j.CreatedOn)
                             .ToListAsync();
        }

        public async Task<Job> GetJobByDisplayIdAsync(Guid displayId, bool includeUserData = false)
        {
            var jobs = _context.Jobs.AsNoTracking();

            if (includeUserData)
                jobs = jobs.Include(j => j.CreatedBy);

            return await jobs.Include(j => j.JobCategory)
                             .Include(j => j.JobSchedules)
                             .SingleOrDefaultAsync(j => j.DisplayId == displayId);
        }

        public async Task<ApplicationResult> AddJobAsync(Job job)
        {
            if (job.DisplayId == Guid.Empty)
            {
                job.DisplayId = Guid.NewGuid();
            }
            job.JobStatus = JobStatus.Available;

            ValidationResult result = _jobValidator.Validate(job);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to create new job with name {JobName}", job.JobName);

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> UpdateJobAsync(Job job)
        {
            int jobId = await _context.Jobs
                .Where(j => j.DisplayId == job.DisplayId)
                .Select(j => j.JobId)
                .FirstOrDefaultAsync();

            if (jobId == 0)
            {
                return ApplicationResult.Failure(new List<string> { $"The job with name {job.JobName} could not be found." });
            }

            job.JobId = jobId;
            ValidationResult result = _jobValidator.Validate(job);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to update job with name {JobName}", job.JobName);

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
