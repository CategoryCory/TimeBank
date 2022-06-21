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
    public class UserSkillsController : ControllerBase
    {
        //private readonly IUserSkillService _userSkillService;
        //private readonly IMapper _mapper;

        //public UserSkillsController(IUserSkillService userSkillService, IMapper mapper)
        //{
        //    _userSkillService = userSkillService;
        //    _mapper = mapper;
        //}

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> GetAllSkills([FromQuery] string searchString)
        //{
        //    var userSkills = await _userSkillService.GetSkillsAsync(searchString);

        //    if (userSkills.Count == 0) return NoContent();

        //    var userSkillsDtos = _mapper.Map<List<UserSkillsDto>>(userSkills);

        //    return Ok(userSkillsDtos);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AddNewSkillRange([FromBody] ICollection<UserSkillsDto> userSkillsDtos)
        //{
        //    var skillsToAdd = _mapper.Map<List<UserSkill>>(userSkillsDtos);

        //    ApplicationResult result = await _userSkillService.AddSkillRangeAsync(skillsToAdd);

        //    if (!result.IsSuccess) return BadRequest(result.Errors);

        //    return NoContent();
        //}
    }
}
