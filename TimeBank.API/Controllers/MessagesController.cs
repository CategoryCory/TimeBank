﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByThread([FromQuery] int threadId)
        {
            // Return error if any parameters are missing
            if (!ModelState.IsValid) return BadRequest();

            // Get all messages in thread
            var messages = await _messageService.GetAllMessagesByThreadAsync(threadId);

            // Create list of dtos to return
            var messagesDtos = _mapper.Map<List<MessageResponseDto>>(messages);

            return Ok(messagesDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMessageToThread([FromBody] MessageDto messageDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var messageToAdd = _mapper.Map<Message>(messageDto);

            var response = await _messageService.AddNewMessageToThreadAsync(messageToAdd, messageToAdd.MessageThreadId);

            if (!response.IsSuccess) return BadRequest(response.Errors);

            return NoContent();
        }

        [HttpPut("read/{messageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetMessageRead(int messageId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _messageService.SetMessageToReadAsync(messageId);

            if (!response.IsSuccess) return BadRequest(response.Errors);

            return NoContent();
        }
    }
}
