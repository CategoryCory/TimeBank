using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.API.Services;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IMapper mapper,
                                 TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto is null || !ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user is null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var userDto = await CreateUserDto(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto is null || !ModelState.IsValid) return BadRequest();

            var user = _mapper.Map<ApplicationUser>(userRegistrationDto);

            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }

            await _userManager.AddToRoleAsync(user, userRegistrationDto.UserRole);

            return StatusCode(StatusCodes.Status201Created);
        }

        private async Task<UserLoginResponseDto> CreateUserDto(ApplicationUser user)
        {
            var token = await _tokenService.CreateToken(user);

            return new UserLoginResponseDto
            {
                DisplayName = string.Empty,
                UserName = user.UserName,
                Token = token
            };
        }
    }
}
