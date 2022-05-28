using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.IdentityModels;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<ApplicationUser> userManager,
                               IUserService userService,
                               IMapper mapper,
                               ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserProfileResponseDto>))]
        public async Task<IActionResult> GetUsers([FromQuery] bool getOnlyUnapproved = false)
        {
            var users = await _userService.GetUsersAsync(getOnlyUnapproved);

            var userResponseDtos = _mapper.Map<List<UserProfileResponseDto>>(users);

            return Ok(userResponseDtos);
        }

        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest();

            var user = await _userService.GetUserByIdAsync(userId);

            if (user is null) return NotFound();

            var userProfileDto = _mapper.Map<UserProfileResponseDto>(user);

            return Ok(userProfileDto);
        }

        [HttpPut("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileUpdateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserById(string userId, [FromBody] UserProfileUpdateDto userUpdateDto)
        {
            // Check if valid userId and userUpdateDto
            if (string.IsNullOrWhiteSpace(userId) || !ModelState.IsValid) return BadRequest();

            // TODO: Check if user to update is the same as the requesting user OR an admin

            // Map userUpdateDto to ApplicationUser
            var userToUpdate = _mapper.Map<ApplicationUser>(userUpdateDto);

            // Set additional fields for userToUpdate
            userToUpdate.Id = userId;

            // We should be able to pass userToUpdate to UserService and have it handle the rest
            ApplicationResult result = await _userService.UpdateUserAsync(userToUpdate);

            // Return error if failure
            if (!result.IsSuccess) return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);

            // If successful, we fetch the user from the db and return Ok
            var updatedUser = await _userManager.FindByIdAsync(userId);
            var responseDto = _mapper.Map<UserProfileResponseDto>(updatedUser);

            return Ok(responseDto);
        }
    }
}
