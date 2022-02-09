using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.Models;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UserRatingsController : ControllerBase
    {
        private readonly IUserRatingService _userRatingService;
        private readonly IMapper _mapper;

        public UserRatingsController(IUserRatingService userRatingService, IMapper mapper)
        {
            _userRatingService = userRatingService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAverageRatingByUserId(string userId)
        {
            var averageRating = await _userRatingService.GetAverageRatingByUserIdAsync(userId);
            return Ok(new UserAverageRatingResponseDto { UserId = userId, AverageRating = averageRating });
        }

        [HttpGet("received/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllReceivedRatingsByUserId(string userId)
        {
            var userRatings = await _userRatingService.GetAllReceivedRatingsByUserIdAsync(userId);

            if (userRatings.Count == 0) return NoContent();

            var userRatingsDtos = _mapper.Map<List<UserRatingsResponseDto>>(userRatings);

            return Ok(userRatingsDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewRating([FromBody] UserRatingsDto userRatingsDto)
        {
            var ratingToCreate = _mapper.Map<UserRating>(userRatingsDto);

            ApplicationResult result = await _userRatingService.AddRatingAsync(ratingToCreate);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
