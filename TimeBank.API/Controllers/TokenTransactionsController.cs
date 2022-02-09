using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.API.Maps;
using TimeBank.Repository.Models;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TokenTransactionsController : ControllerBase
    {
        private readonly ITokenTransactionService _tokenTransactionService;
        private readonly ITokenBalanceService _tokenBalanceService;
        private readonly IMapper _mapper;

        public TokenTransactionsController(ITokenTransactionService tokenTransactionService,
                                           ITokenBalanceService tokenBalanceService,
                                           IMapper mapper)
        {
            _tokenTransactionService = tokenTransactionService;
            _tokenBalanceService = tokenBalanceService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            var transactions = await _tokenTransactionService.GetAllTransactionsByUserIdAsync(userId);

            if (transactions.Count == 0) return NoContent();

            List<TokenTransactionResponseDto> transactionDtos = TokenTransactionResponseMap.MapToDto(transactions);

            return Ok(transactionDtos);
        }
        
        [HttpGet("balance/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentBalanceByUserId(string userId)
        {
            var tokenBalance = await _tokenBalanceService.GetBalanceByUserId(userId);

            if (tokenBalance is null) return NotFound();

            return Ok(new TokenBalanceResponseDto { UserId = tokenBalance.UserId, CurrentBalance = tokenBalance.CurrentBalance });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewTransaction(TokenTransactionDto transactionDto)
        {
            TokenTransaction transactionToAdd = _mapper.Map<TokenTransaction>(transactionDto);

            ApplicationResult result = await _tokenTransactionService.AddNewTransactionAsync(transactionToAdd);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
