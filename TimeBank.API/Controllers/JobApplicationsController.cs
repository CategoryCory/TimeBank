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
    [Authorize]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IMapper _mapper;

        public JobApplicationsController(IJobApplicationService jobApplicationService, IMapper mapper)
        {
            _jobApplicationService = jobApplicationService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobApplicationById(int id)
        {
            var jobApplication = await _jobApplicationService.GetApplicationByIdAsync(id);

            if (jobApplication is null) return NotFound();

            var jobApplicationDto = _mapper.Map<JobApplicationResponseDto>(jobApplication);

            return Ok(jobApplicationDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobApplicationsByUserId([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            var jobApplications = await _jobApplicationService.GetAllApplicationsByUserIdAsync(userId);

            if (jobApplications.Count == 0) return NotFound();

            var jobApplicationDtos = _mapper.Map<List<JobApplicationResponseDto>>(jobApplications);

            return Ok(jobApplicationDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddJobApplication([FromBody] JobApplicationDto jobApplicationDto)
        {
            JobApplication jobApplication = new() 
            { 
                JobDisplayId = jobApplicationDto.JobDisplayId,
                ApplicantId = jobApplicationDto.ApplicantId
            };

            ApplicationResult result = await _jobApplicationService.AddJobApplicationAsync(jobApplication);

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
