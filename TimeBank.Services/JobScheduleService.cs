using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class JobScheduleService : IJobScheduleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobScheduleService> _logger;
        private readonly JobScheduleValidator _validator;

        public JobScheduleService(ApplicationDbContext context, ILogger<JobScheduleService> logger)
        {
            _context = context;
            _logger = logger;
            _validator = new JobScheduleValidator();
        }

        public async Task<List<JobSchedule>> GetJobSchedulesByJobIdAsync(int jobId)
        {
            var jobSchedules = await _context.JobSchedules.AsNoTracking().Where(s => s.JobId == jobId).ToListAsync();

            return jobSchedules;
        }

        public async Task<JobSchedule> GetJobScheduleByIdAsync(int jobScheduleId)
        {
            var jobSchedule = await _context.JobSchedules.FindAsync(jobScheduleId);

            return jobSchedule;
        }

        public async Task<ApplicationResult> AddJobScheduleRangeAsync(ICollection<JobSchedule> jobSchedules)
        {
            foreach (var schedule in jobSchedules)
            {
                ValidationResult result = _validator.Validate(schedule);

                if (!result.IsValid)
                {
                    _logger.LogError("Could not create schedule.");
                    return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
                }
            }

            _context.JobSchedules.AddRange(jobSchedules);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
