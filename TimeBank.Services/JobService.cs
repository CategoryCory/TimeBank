using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Entities.Models;
using TimeBank.Repository;
using TimeBank.Services.Contracts;

namespace TimeBank.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobService> _logger;

        public JobService(ApplicationDbContext context, ILogger<JobService> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task CreateNewJobAsync(Job job)
        {
            job.DisplayId = new Guid();
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            if (await _context.Jobs.AnyAsync(j => j.JobId == job.JobId) == false)
            {
                return;
            }

            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJobAsync(Job job)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }
}
