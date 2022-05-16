using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class MessageThreadService : IMessageThreadService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageThreadService> _logger;
        private readonly MessageThreadValidator _validator;

        public MessageThreadService(ApplicationDbContext context, ILogger<MessageThreadService> logger)
        {
            _context = context;
            _logger = logger;
            _validator = new MessageThreadValidator();
        }

        public async Task<MessageThread> GetMessageThreadByIdAsync(int threadId)
        {
            var messageThread = await _context.MessageThreads.FindAsync(threadId);

            return messageThread;
        }

        public async Task<MessageThread> GetMessageThreadByJobAndParticipantsAsync(int jobId, string jobApplicantId)
        {
            var messageThread = await _context.MessageThreads.Where(x => x.JobId == jobId
                                                                         && (x.ToUserId == jobApplicantId || x.FromUserId == jobApplicantId))
                                                             .SingleOrDefaultAsync();

            return messageThread;
        }

        public async Task<ApplicationResult> CreateMessageThreadAsync(MessageThread messageThread)
        {
            ValidationResult result = _validator.Validate(messageThread);

            if (!result.IsValid)
            {
                _logger.LogError("There was an error creating a message thread for job {jobId}", messageThread.JobId);

                return ApplicationResult.Failure(result.Errors.Select(m => m.ErrorMessage).ToList());
            }

            _context.MessageThreads.Add(messageThread);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
