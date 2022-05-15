using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageThreadsController : ControllerBase
    {
        private readonly IMessageThreadService _messageThreadService;
        private readonly IMapper _mapper;

        public MessageThreadsController(IMessageThreadService messageThreadService, IMapper mapper)
        {
            _messageThreadService = messageThreadService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessageThread([FromQuery] int jobId,
                                                          [FromQuery] string toUserId,
                                                          [FromQuery] string fromUserId)
        {
            var messageThread = await _messageThreadService.GetMessageThreadByJobAndParticipantsAsync(jobId, toUserId, fromUserId);

            if (messageThread is null) return NotFound();

            var threadResponseDto = _mapper.Map<MessageThreadResponseDto>(messageThread);

            return Ok(threadResponseDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMessageThread([FromBody] MessageThreadDto messageThreadDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var messageToAdd = _mapper.Map<MessageThread>(messageThreadDto);

            var result = await _messageThreadService.CreateMessageThreadAsync(messageToAdd);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
