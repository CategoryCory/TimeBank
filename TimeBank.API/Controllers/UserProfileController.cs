using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(UserManager<ApplicationUser> userManager,
                                     IUserService userService,
                                     IMapper mapper,
                                     ILogger<UserProfileController> logger)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<UserProfileController>/<guid>
        //[HttpGet("{userId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetProfileById(string userId)
        //{
        //    var user = await _userManager.Users.Include(u => u.Skills).SingleOrDefaultAsync(u => u.Id == userId);

        //    if (user is null) return NotFound();

        //    var userProfileDto = _mapper.Map<UserProfileResponseDto>(user);
        //    return Ok(userProfileDto);
        //}

        // PUT api/<UserProfileController>/<guid>
        //[HttpPut("{userId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdateProfileById(string userId, [FromBody] UserProfileUpdateDto userProfileDto)
        //{
        //    // Check if valid userId
        //    if (string.IsNullOrWhiteSpace(userId)) return BadRequest();

        //    // Get user
        //    var user = await _userManager.Users.Include(u => u.Skills).SingleOrDefaultAsync(u => u.Id == userId);

        //    // If user isn't found, bail
        //    if (user is null) return NotFound();

        //    // Update user's skills
        //    var skills = _mapper.Map<List<UserSkill>>(userProfileDto.Skills);
        //    ApplicationResult resultSkillSet = await _userService.SetUserSkillsAsync(user, skills);

        //    if (!resultSkillSet.IsSuccess) return BadRequest(resultSkillSet.Errors);

        //    // Update user info
        //    _mapper.Map(userProfileDto, user);

        //    await _userManager.UpdateAsync(user);
        //    return Ok(userProfileDto);
        //}
    }
}
