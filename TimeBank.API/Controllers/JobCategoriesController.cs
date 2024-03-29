﻿using AutoMapper;
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
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly IJobCategoryService _jobCategoryService;
        private readonly IMapper _mapper;

        public JobCategoriesController(IJobCategoryService jobCategoryService, IMapper mapper)
        {
            _jobCategoryService = jobCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<JobCategoryResponseDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllJobCategoriesAsync()
        {
            var jobCategories = await _jobCategoryService.GetAllCategoriesAsync();

            if (jobCategories.Count == 0) return NoContent();

            var jobCategoryDtos = _mapper.Map<List<JobCategoryResponseDto>>(jobCategories);

            return Ok(jobCategoryDtos);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobCategoryResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobCategoryByIdAsync(int categoryId)
        {
            var jobCategory = await _jobCategoryService.GetCategoryByIdAsync(categoryId);

            if (jobCategory is null) return NotFound();

            var jobCategoryDto = _mapper.Map<JobCategoryResponseDto>(jobCategory);

            return Ok(jobCategoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddJobCategoryAsync(JobCategoryDto jobCategoryDto)
        {
            var jobCategoryToAdd = _mapper.Map<JobCategory>(jobCategoryDto);

            ApplicationResult result = await _jobCategoryService.AddJobCategoryAsync(jobCategoryToAdd);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
