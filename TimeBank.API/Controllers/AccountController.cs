﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.API.Services;
using TimeBank.Repository.IdentityModels;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ITokenBalanceService _tokenBalanceService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IConfiguration config,
                                 IMapper mapper,
                                 ITokenService tokenService,
                                 ITokenBalanceService tokenBalanceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _mapper = mapper;
            _tokenService = tokenService;
            _tokenBalanceService = tokenBalanceService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto is null || !ModelState.IsValid) return BadRequest();

            var user = await _userManager.Users.AsNoTracking()
                                               .Where(u => u.Email == userLoginDto.Email)
                                               .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                                               .SingleOrDefaultAsync();

            if (user is null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var token = await _tokenService.CreateToken(user);

            Response.Cookies.Append(_config["JwtSettings:CookieName"],
                                    token,
                                    new CookieOptions()
                                    {
                                        HttpOnly = true,
                                        Secure = true,
                                        SameSite = SameSiteMode.Strict,
                                        Expires = DateTimeOffset.Now.AddDays(_config.GetSection("JwtSettings:ExpiresInMinutes")
                                                                                    .Get<int>())
                                    });

            //Response.Cookies.Append(_config["RefreshTokenSettings:CookieName"],
            //    )

            var userDto = await CreateUserLoginResponseDto(user);

            return Ok(userDto);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            //if (string.IsNullOrEmpty(Request.Cookies[_config["JwtCookieName"]])) return BadRequest();

            //Response.Cookies.Delete(_config["JwtCookieName"]);

            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies[_config["RefreshTokenCookieName"]];



            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.AsNoTracking()
                                               .Where(u => u.Email == userEmail)
                                               .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                                               .SingleOrDefaultAsync();

            if (user is null) return NotFound();

            var userDto = await CreateUserLoginResponseDto(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserProfileResponseDto))]
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

            if (await _roleManager.RoleExistsAsync("Pending") == false)
            {
                var userRole = new IdentityRole { Name = "Pending", NormalizedName = "PENDING" };
                await _roleManager.CreateAsync(userRole);
            }

            await _userManager.AddToRoleAsync(user, "Pending");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));

            await _tokenBalanceService.CreateNewBalance(user.Id);

            var actionName = nameof(GetUserProfile);
            var routeValues = new { userId = user.Id };
            var createdUser = _mapper.Map<UserProfileResponseDto>(user);

            return CreatedAtAction(actionName, routeValues, createdUser);
        }

        [HttpPut("approve/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var userToApprove = await _userManager.FindByIdAsync(userId);

            if (userToApprove is null) return NotFound();

            await _userManager.AddToRoleAsync(userToApprove, "User");
            await _userManager.RemoveFromRoleAsync(userToApprove, "Pending");

            userToApprove.IsApproved = true;
            await _userManager.UpdateAsync(userToApprove);

            return NoContent();
        }

        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            if (userId is null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) return NotFound();

            var userProfileDto = _mapper.Map<UserProfileResponseDto>(user);

            return Ok(userProfileDto);
        }

        private async Task<UserLoginResponseDto> CreateUserLoginResponseDto(ApplicationUser user)
        {
            //var token = await _tokenService.CreateToken(user);

            var roles = await _userManager.GetRolesAsync(user);

            var profileImageUrl = user.Photos.Count > 0 ? user.Photos.First().URL : "";

            return new UserLoginResponseDto
            {
                IsAuthenticationSuccessful = true,
                ErrorMessage = string.Empty,
                UserId = user.Id,
                DisplayName = user.FirstName,
                UserName = user.UserName,
                Email = user.Email,
                ProfileImageUrl = profileImageUrl,
                Roles = roles,
                IsApproved = user.IsApproved,
                //Token = token
            };
        }
    }
}
