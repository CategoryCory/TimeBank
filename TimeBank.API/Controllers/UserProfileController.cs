using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(UserManager<ApplicationUser> userManager,
                                     IMapper mapper,
                                     ILogger<UserProfileController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<UserProfileController>/test@test.com
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfileByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return NotFound();

            var userProfileDto = _mapper.Map<UserProfileResponseDto>(user);
            return Ok(userProfileDto);
        }

        // PUT api/<UserProfileController>/test@test.com
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfileByEmailAsync(string email, [FromBody] UserProfileUpdateDto userProfileDto)
        {
            string decodedEmail = WebUtility.UrlDecode(email);
            var user = await _userManager.FindByEmailAsync(decodedEmail);

            if (user is null) return NotFound();

            _mapper.Map(userProfileDto, user);

            await _userManager.UpdateAsync(user);
            return Ok(userProfileDto);
        }
    }
}
