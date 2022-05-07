using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.Models;
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
        public async Task<IActionResult> GetMessagesByThread([FromBody] MessageThreadDto messageThreadDto)
        {
            // Return error if any parameters are missing
            if (!ModelState.IsValid) return BadRequest();

            // Get message thread
            var messageThread = await _messageService.GetMessageThreadByJobAndParticipantsAsync(
                messageThreadDto.JobId,
                messageThreadDto.ToUserId,
                messageThreadDto.FromUserId);

            // If message thread doesn't exist, return
            if (messageThread is null) return NotFound();

            // Get all messages in thread
            var messages = await _messageService.GetAllMessagesByThreadAsync(messageThread.MessageThreadId);

            // Create list of dtos to return
            var messagesDtos = _mapper.Map<MessageResponseDto>(messages);

            return Ok(messagesDtos);
        }

        [HttpPost("thread")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMessageThread([FromBody] MessageThreadDto messageThreadDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var messageToAdd = _mapper.Map<MessageThread>(messageThreadDto);

            var result = await _messageService.CreateMessageThreadAsync(messageToAdd);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
