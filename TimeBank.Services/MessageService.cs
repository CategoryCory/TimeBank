using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageService> _logger;
        private readonly MessageThreadValidator _threadValidator;
        private readonly MessageValidator _messageValidator;

        public MessageService(ApplicationDbContext context, ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
            _threadValidator = new MessageThreadValidator();
            _messageValidator = new MessageValidator();
        }

        public async Task<MessageThread> GetMessageThreadByIdAsync(int threadId)
        {
            var messageThread = await _context.MessageThreads.FindAsync(threadId);

            return messageThread;
        }

        public async Task<MessageThread> GetMessageThreadByJobAndParticipantsAsync(int jobId, string toUserId, string fromUserId)
        {
            var messageThread = await _context.MessageThreads.Where(x => x.JobId == jobId && x.ToUserId == toUserId && x.FromUserId == fromUserId)
                                                             .SingleOrDefaultAsync();

            return messageThread;
        }

        public async Task<List<Message>> GetAllMessagesByThreadAsync(int threadId)
        {
            var messages = await _context.Messages.AsNoTracking().Where(m => m.MessageThreadId == threadId).ToListAsync();

            return messages;
        }

        public async Task<ApplicationResult> CreateMessageThreadAsync(MessageThread messageThread)
        {
            ValidationResult result = _threadValidator.Validate(messageThread);

            if (!result.IsValid)
            {
                _logger.LogError("There was an error creating a message thread for job {jobId}", messageThread.JobId);

                return ApplicationResult.Failure(result.Errors.Select(m => m.ErrorMessage).ToList());
            }

            _context.MessageThreads.Add(messageThread);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }

        public async Task<ApplicationResult> AddNewMessageToThreadAsync(Message message, int threadId)
        {
            ValidationResult result = _messageValidator.Validate(message);

            if (!result.IsValid)
            {
                _logger.LogError("There was a error adding message to thread {threadId}", threadId);

                return ApplicationResult.Failure(result.Errors.Select(m => m.ErrorMessage).ToList());
            }

            try
            {
                message.IsRead = false;

                _context.Messages.Add(message);

                await _context.SaveChangesAsync();

                return ApplicationResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("A database error occurred: {errorMessage}", ex.Message);
                return ApplicationResult.Failure(new List<string> { ex.Message });
            }
        }

        public async Task<ApplicationResult> SetMessageToReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);

            if (message is null)
            {
                _logger.LogError("No message with id {messageId} exists", messageId);
                return ApplicationResult.Failure(new List<string> { $"No message with id {messageId} exists" });
            }

            message.IsRead = true;
            message.ReadOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return ApplicationResult.Success();
        }
    }
}
