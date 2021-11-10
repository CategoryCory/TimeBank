using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();
            return Ok(jobs);
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JobsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
