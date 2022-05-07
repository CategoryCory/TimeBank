using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TimeBank.API.Hubs.Clients;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Hubs
{
    public class MessagesHub : Hub<IMessageClient>
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessagesHub> _logger;

        public MessagesHub(IMessageService messageService, ILogger<MessagesHub> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task SendMessage(string userId, Message message)
        {
            // Save message to database
            var result = await _messageService.AddNewMessageToThreadAsync(message, message.MessageThreadId);

            if (!result.IsSuccess)
            {
                _logger.LogError("Message could not be added to thread {threadId}", message.MessageThreadId);
                return;
            }

            // Send message to appropriate user
            await Clients.User(userId).ReceiveMessage(message);
        }
    }
}
