using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("thread")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByThread([FromQuery] int jobId = 0,
                                                             [FromQuery] string toUserId = "",
                                                             [FromQuery] string fromUserId = "")
        {
            // Return error if any parameters are missing
            if (jobId == 0 || toUserId == "" || fromUserId == "") return BadRequest();

            // Get message thread
            var messageThread = await _messageService.GetMessageThreadByJobAndParticipantsAsync(jobId, toUserId, fromUserId);

            // If message thread doesn't exist, return
            if (messageThread is null) return NotFound();

            // Get all messages in thread
            var messages = await _messageService.GetAllMessagesByThreadAsync(messageThread.MessageThreadId);

            // Create list of dtos to return
            var messagesDtos = new List<MessageResponseDto>();
            foreach (var message in messages)
            {
                messagesDtos.Add(new MessageResponseDto
                {
                    MessageId = message.MessageId,
                    IsFromSender = message.IsFromSender,
                    CreatedOn = message.CreatedOn,
                    IsRead = message.IsRead,
                    ReadOn = message.ReadOn,
                    Body = message.Body,
                    AuthorId = message.AuthorId,
                    RecipientId = message.IsFromSender ? messageThread.ToUserId : messageThread.FromUserId,
                    MessageThreadId = message.MessageThreadId,
                    JobId = messageThread.JobId
                });
            }

            return Ok(messagesDtos);
        }
    }
}
