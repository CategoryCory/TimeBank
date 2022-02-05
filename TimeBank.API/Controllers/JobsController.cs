﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly ILogger<JobsController> _logger;

        public JobsController(UserManager<ApplicationUser> userManager,
                              IJobService jobService,
                              IMapper mapper,
                              ILogger<JobsController> logger)
        {
            _userManager = userManager;
            _jobService = jobService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<JobsController>/?userId=GUID
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllJobs([FromQuery] string userId)
        {
            bool isAuthenticatedAndApproved = await CheckIfAuthenticatedAndApproved(User.FindFirstValue(ClaimTypes.Email));

            var jobs = await _jobService.GetAllJobsAsync(userId, isAuthenticatedAndApproved);

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
            bool isAuthenticatedAndApproved = await CheckIfAuthenticatedAndApproved(User.FindFirstValue(ClaimTypes.Email));

            var job = await _jobService.GetJobByDisplayIdAsync(displayId, isAuthenticatedAndApproved);

            if (job is null) return NotFound();

            var jobResponseDto = _mapper.Map<JobResponseDto>(job);

            return Ok(jobResponseDto);
        }

        // POST api/<JobsController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewJob([FromBody] JobDto jobDto)
        {
            var jobToCreate = _mapper.Map<Job>(jobDto);

            ApplicationResult result = await _jobService.AddJobAsync(jobToCreate);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // PUT api/<JobsController>/5
        [HttpPut]
        [Authorize]
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
        [Authorize]
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

        private async Task<bool> CheckIfAuthenticatedAndApproved(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail)) return false;

            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            if (currentUser is not null && currentUser.IsApproved)
            {
                return true;
            }

            return false;
        }
    }
}
