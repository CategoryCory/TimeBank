using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobApplicationService> _logger;
        private readonly JobApplicationValidator _validator;

        public JobApplicationService(ApplicationDbContext context, ILogger<JobApplicationService> logger)
        {
            _context = context;
            _logger = logger;
            _validator = new JobApplicationValidator();
        }

        public async Task<List<JobApplication>> GetJobApplicationsAsync(string userId)
        {
            var apps = _context.JobApplications.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                apps = apps.Where(j => j.ApplicantId == userId);
            }

            return await apps.Include(j => j.Job)
                             .ThenInclude(j => j.CreatedBy)
                             .Include(j => j.Job)
                             .ThenInclude(j => j.JobCategory)
                             .Include(j => j.Applicant)
                             .ToListAsync();
        }

        public async Task<List<JobApplication>> GetJobApplicationsByJobAsync(int jobId)
        {
            return await _context.JobApplications
                                 .AsNoTracking()
                                 .Include(a => a.Applicant)
                                 .ThenInclude(a => a.Skills)
                                 .Include(a => a.JobApplicationSchedule)
                                 .Where(j => j.JobId == jobId)
                                 .ToListAsync();
        }

        public async Task<DateTime?> CheckApplicationDateByJobAndUserAsync(string userId, int jobId)
        {
            var applicationExists = await _context.JobApplications.AnyAsync(j => j.ApplicantId == userId && j.JobId == jobId);

            if (!applicationExists) return null;

            return await _context.JobApplications.AsNoTracking()
                                                 .Where(j => j.ApplicantId == userId && j.JobId == jobId)
                                                 .Select(j => j.CreatedOn)
                                                 .FirstOrDefaultAsync();
        }

        public async Task<ApplicationResult> AddJobApplicationAsync(JobApplication jobApplication)
        {
            ValidationResult result = _validator.Validate(jobApplication);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to create a job response for job {}.", jobApplication.JobId);

                return ApplicationResult.Failure(result.Errors.Select(x => x.ErrorMessage).ToList());
            }

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();

            //using var dbTransaction = _context.Database.BeginTransaction();

            //try
            //{
            //    jobApplication.JobApplicationSchedules = new List<JobSchedule>();
            //    _context.JobApplications.Add(jobApplication);
            //    await _context.SaveChangesAsync();

            //    if (jobAppScheduleIds != null && jobAppScheduleIds.Count > 0)
            //    {
            //        foreach (int scheduleId in jobAppScheduleIds)
            //        {
            //            var s = await _context.JobSchedules.FindAsync(scheduleId);
            //            jobApplication.JobApplicationSchedules.Add(s);
            //        }

            //        await _context.SaveChangesAsync();
            //    }

            //    await dbTransaction.CommitAsync();
            //    return ApplicationResult.Success();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Adding the job application failed: {}", ex.InnerException);
            //    return ApplicationResult.Failure(new List<string> { $"Adding the job application failed: {ex.InnerException.Message}" });
            //}
        }

        public async Task<ApplicationResult> EditJobApplicationStatusByIdAsync(int id, string newStatus)
        {
            if (Enum.TryParse(newStatus, out JobApplicationStatus enumNewStatus))
            {
                var jobResponse = await _context.JobApplications.FindAsync(id);

                if (jobResponse is null)
                {
                    _logger.LogError("The job with id {} could not be found.", id);
                    return ApplicationResult.Failure(new List<string> { $"The job with id {id} could not be found." });
                }

                jobResponse.Status = enumNewStatus;

                if (enumNewStatus == JobApplicationStatus.Completed || enumNewStatus == JobApplicationStatus.Declined)
                {
                    jobResponse.ResolvedOn = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                return ApplicationResult.Success();
            }

            _logger.LogError("The status {} is not a valid status.", newStatus);
            return ApplicationResult.Failure(new List<string> { $"The status '{newStatus}' is not a valid status." });
        }
    }
}
