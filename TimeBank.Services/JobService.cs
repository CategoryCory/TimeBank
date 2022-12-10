using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public sealed class JobService : IJobService
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

        public async Task<List<Job>> GetAllJobsAsync(int page,
                                                     int perPage,
                                                     string userId = null,
                                                     bool includeUserData = false,
                                                     bool includeClosed = false)
        {
            var jobs = _context.Jobs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(userId))
                jobs = jobs.Where(j => j.CreatedById == userId);

            if (includeUserData)
                jobs = jobs.Include(j => j.CreatedBy);

            if (!includeClosed)
                jobs = jobs.Where(j => j.JobStatus == JobStatus.Available);

            return await jobs.Include(j => j.JobCategory)
                             .Include(j => j.JobSchedules)
                             .AsSplitQuery()
                             .OrderByDescending(j => j.CreatedOn)
                             .Skip((page - 1) * perPage)
                             .Take(perPage)
                             .ToListAsync();
        }

        public async Task<Job> GetJobByIdAsync(int id, bool includeUserData = false)
        {
            var jobs = _context.Jobs.AsNoTracking();

            if (includeUserData)
            {
                jobs = jobs.Include(j => j.CreatedBy)
                           .Include(j => j.JobApplications)
                           .ThenInclude(a => a.Applicant)
                           .ThenInclude(u => u.Skills)
                           .Include(j => j.JobApplications)
                           .ThenInclude(a => a.JobApplicationSchedule);
            }

            return await jobs.Include(j => j.JobCategory)
                             .Include(j => j.JobSchedules)
                             .AsSplitQuery()
                             .FirstOrDefaultAsync(j => j.JobId == id);
        }

        public async Task<ApplicationResult> AddJobAsync(Job job)
        {
            //if (job.DisplayId == Guid.Empty)
            //{
            //    job.DisplayId = Guid.NewGuid();
            //}
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

        public async Task<ApplicationResult> UpdateJobAsync(Job jobToUpdate)
        {
            // Load the current job from the database
            //var jobFromDb = await _context.Jobs
            //    .Where(j => j.DisplayId == jobToUpdate.DisplayId)
            //    .Include(j => j.JobSchedules)
            //    .SingleOrDefaultAsync();

            var jobFromDb = await _context.Jobs
                .Include(j => j.JobSchedules)
                .FirstOrDefaultAsync(j => j.JobId == jobToUpdate.JobId);

            // If not found, bail
            if (jobFromDb is null || jobFromDb.JobId == 0)
            {
                return ApplicationResult.Failure(new List<string> { $"The job with name {jobToUpdate.JobName} could not be found." });
            }

            // Job is found -- copy updated values
            jobFromDb.JobName = jobToUpdate.JobName;
            jobFromDb.Description = jobToUpdate.Description;
            jobFromDb.ExpiresOn = jobToUpdate.ExpiresOn;
            jobFromDb.JobCategoryId = jobToUpdate.JobCategoryId;
            jobFromDb.JobScheduleType = jobToUpdate.JobScheduleType;

            // If incoming job has a custom schedule type, add and remove schedule entries as needed
            if (jobToUpdate.JobScheduleType == JobScheduleType.Custom)
            {
                // Check job schedules against incoming schedules and remove as necessary
                foreach (var schedule in jobFromDb.JobSchedules)
                {
                    if (!jobToUpdate.JobSchedules.Contains(schedule)) jobFromDb.JobSchedules.Remove(schedule);
                }

                // Check incoming schedule entries and add as needed
                foreach (var schedule in jobToUpdate.JobSchedules)
                {
                    if (!jobFromDb.JobSchedules.Contains(schedule)) jobFromDb.JobSchedules.Add(schedule);
                }
            }

            // If incoming job has an open schedule type, remove job schedules
            if (jobToUpdate.JobScheduleType == JobScheduleType.Open)
            {
                jobFromDb.JobSchedules.Clear();
            }

            // Validate and bail if errors
            ValidationResult result = _jobValidator.Validate(jobFromDb);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to update job with name {JobName}", jobFromDb.JobName);

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            // No errors -- save changes and return success
            _context.Jobs.Update(jobFromDb);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> DeleteJobAsync(int jobId)
        {
            //var jobToDelete = await _context.Jobs.FirstOrDefaultAsync(j => j.DisplayId == displayId);
            var jobToDelete = await _context.Jobs.FindAsync(jobId);

            if (jobToDelete is null)
            {
                return ApplicationResult.Failure(new List<string> { $"The job with id {jobId} could not be found." });
            }

            _context.Jobs.Remove(jobToDelete);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
