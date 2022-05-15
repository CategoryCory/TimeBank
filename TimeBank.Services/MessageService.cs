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
        private readonly MessageValidator _messageValidator;

        public MessageService(ApplicationDbContext context, ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
            _messageValidator = new MessageValidator();
        }

        public async Task<List<Message>> GetAllMessagesByThreadAsync(int threadId)
        {
            var messages = await _context.Messages.AsNoTracking().Where(m => m.MessageThreadId == threadId).ToListAsync();

            return messages;
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
