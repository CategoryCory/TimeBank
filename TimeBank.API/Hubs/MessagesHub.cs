using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.API.Hubs.Clients;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Hubs
{
    public class MessagesHub : Hub<IMessageClient>
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly ILogger<MessagesHub> _logger;

        public MessagesHub(IMessageService messageService, IMapper mapper, ILogger<MessagesHub> logger)
        {
            _messageService = messageService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SendMessage(string userId, MessageDto messageDto)
        {
            var messageToAdd = _mapper.Map<Message>(messageDto);

            // Save message to database
            var result = await _messageService.AddNewMessageToThreadAsync(messageToAdd, messageToAdd.MessageThreadId);

            if (!result.IsSuccess)
            {
                _logger.LogError("Message could not be added to thread {threadId}", messageToAdd.MessageThreadId);
                return;
            }

            // Send message to appropriate user
            var messageToSend = _mapper.Map<MessageResponseDto>(messageToAdd);

            await Clients.User(userId).ReceiveMessage(messageToSend);
        }
    }
}
