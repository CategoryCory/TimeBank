using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobsController(IJobService jobService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/<JobsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllJobs()
        {
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            bool isAuthenticatedAndApproved = !string.IsNullOrWhiteSpace(userRole) && userRole != "Pending";

            var jobs = await _jobService.GetAllJobsAsync(includeUserData: isAuthenticatedAndApproved);

            if (jobs.Count == 0) return NoContent();

            var jobResponseDtos = _mapper.Map<List<JobResponseDto>>(jobs);

            return Ok(jobResponseDtos);
        }

        // GET api/<JobsController>/currentuser
        [HttpGet("currentuser")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUserJobs()
        {
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail)) return BadRequest();

            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

            bool isAuthenticatedAndApproved = (currentUser is not null) && currentUserRole != "Pending";

            if (!isAuthenticatedAndApproved) return Unauthorized();

            var jobs = await _jobService.GetAllJobsAsync(currentUser.Id, isAuthenticatedAndApproved);

            if (jobs.Count == 0) return NoContent();

            var jobResponseDtos = _mapper.Map<List<JobResponseDto>>(jobs);

            return Ok(jobResponseDtos);
        }

        // GET api/<JobsController>/GUID
        [HttpGet("{displayId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid displayId)
        {
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            bool isAuthenticatedAndApproved = !string.IsNullOrWhiteSpace(userRole) && userRole != "Pending";

            var job = await _jobService.GetJobByDisplayIdAsync(displayId, isAuthenticatedAndApproved);

            if (job is null) return NotFound();

            var jobResponseDto = _mapper.Map<JobResponseDto>(job);

            return Ok(jobResponseDto);
        }

        // POST api/<JobsController>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewJob([FromBody] JobDto jobDto)
        {
            var jobToCreate = _mapper.Map<Job>(jobDto);

            var creator = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (creator is null) return BadRequest("User could not be found.");

            jobToCreate.CreatedById = creator.Id;

            ApplicationResult result = await _jobService.AddJobAsync(jobToCreate);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // PUT api/<JobsController>/5
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateJob([FromBody] JobDto jobDto)
        {
            var jobToUpdate = _mapper.Map<Job>(jobDto);

            ApplicationResult result = await _jobService.UpdateJobAsync(jobToUpdate);

            if (result is null)
            {
                return NotFound();
            }

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{displayId}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJob(Guid displayId)
        {
            ApplicationResult result = await _jobService.DeleteJobAsync(displayId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return NoContent();
        }
    }
}
