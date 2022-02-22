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
    public class JobScheduleController : ControllerBase
    {
        private readonly IJobScheduleService _jobScheduleService;
        private readonly IMapper _mapper;

        public JobScheduleController(IJobScheduleService jobScheduleService, IMapper mapper)
        {
            _jobScheduleService = jobScheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetJobSchedulesByJobId(int jobId)
        {
            var jobSchedules = await _jobScheduleService.GetJobSchedulesByJobIdAsync(jobId);

            if (jobSchedules.Count == 0) return NoContent();

            var jobScheduleDtos = _mapper.Map<JobScheduleDto>(jobSchedules);

            return Ok(jobScheduleDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddJobScheduleRange([FromBody] ICollection<JobScheduleDto> jobScheduleDtos)
        {
            var schedulesToAdd = _mapper.Map<ICollection<JobSchedule>>(jobScheduleDtos);

            ApplicationResult result = await _jobScheduleService.AddJobScheduleRangeAsync(schedulesToAdd);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
