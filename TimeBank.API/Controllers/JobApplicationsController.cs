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
    [Authorize]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public JobApplicationsController(IJobApplicationService jobApplicationService,
                                         UserManager<ApplicationUser> userManager,
                                         IMapper mapper)
        {
            _jobApplicationService = jobApplicationService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckApplicationByJobId([FromQuery] int jobId)
        {
            var applicant = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            var jobApplicationDate = await _jobApplicationService.CheckApplicationDateByJobAndUserAsync(applicant.Id, jobId);

            return Ok(new
            {
                ApplicationExists = jobApplicationDate.HasValue,
                ApplicationDate = jobApplicationDate ?? DateTime.MinValue,
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddJobApplication([FromBody] JobApplicationDto jobApplicationDto)
        {
            var applicant = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            JobApplication jobApplication = new() 
            {
                JobId = jobApplicationDto.JobId,
                ApplicantId = applicant.Id,
            };

            ApplicationResult result = await _jobApplicationService.AddJobApplicationAsync(jobApplication, jobApplicationDto.JobSchedules);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateJobApplicationStatusById([FromBody] JobApplicationStatusUpdateDto statusUpdateDto)
        {
            var result = await _jobApplicationService.EditJobApplicationStatusByIdAsync(statusUpdateDto.JobApplicationId,
                                                                                  statusUpdateDto.Status);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
