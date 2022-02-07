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

        public async Task<List<JobApplication>> GetAllApplicationsByUserIdAsync(string userId)
        {
            return await _context.JobApplications.AsNoTracking()
                                              .Include(r => r.Job)
                                              .Where(r => r.ApplicantId == userId)
                                              .ToListAsync();
        }

        public async Task<JobApplication> GetApplicationByIdAsync(int id)
        {
            return await _context.JobApplications.AsNoTracking()
                                              .SingleOrDefaultAsync(r => r.JobApplicationId == id);
        }

        public async Task<ApplicationResult> AddJobApplicationAsync(JobApplication jobApplication)
        {
            ValidationResult result = _validator.Validate(jobApplication);

            if (!result.IsValid)
            {
                _logger.LogError("Failed to create a job response for job {}.", jobApplication.JobDisplayId);

                return ApplicationResult.Failure(result.Errors.Select(x => x.ErrorMessage).ToList());
            }

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
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
