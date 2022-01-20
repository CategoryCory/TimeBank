using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
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

        public JobsController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        // GET: api/<JobsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();

            if (jobs.Count == 0)
            {
                return NoContent();
            }

            return Ok(jobs);
        }

        // GET api/<JobsController>/5
        [HttpGet("{displayId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid displayId)
        {
            var job = await _jobService.GetJobByDisplayIdAsync(displayId);

            if (job is null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // POST api/<JobsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewJob([FromBody] JobDto jobDto)
        {
            var jobToCreate = _mapper.Map<Job>(jobDto);

            ApplicationResult result = await _jobService.CreateNewJobAsync(jobToCreate);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // PUT api/<JobsController>/5
        [HttpPut]
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
