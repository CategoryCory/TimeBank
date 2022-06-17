using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TimeBank.API.Dtos;
using TimeBank.API.Services;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly IPhotoUploadService _photoUploadService;
    private readonly HtmlEncoder _htmlEncoder;

    public PhotosController(UserManager<ApplicationUser> userManager,
                            IMapper mapper,
                            IPhotoService photoService,
                            IPhotoUploadService photoUploadService,
                            HtmlEncoder htmlEncoder)
    {
        _userManager = userManager;
        _mapper = mapper;
        _photoService = photoService;
        _photoUploadService = photoUploadService;
        _htmlEncoder = htmlEncoder;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhotoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentPhotoByUserId([FromQuery] string userId)
    {
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest("A user ID must be provided.");

        var photo = await _photoService.GetCurrentPhotoByUserIdAsync(userId);

        if (photo is null) return NotFound();

        var photoResponseDto = _mapper.Map<PhotoResponseDto>(photo);

        return Ok(photoResponseDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhotoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadPhoto()
    {
        var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrWhiteSpace(currentUserEmail)) return BadRequest("Invalid user.");

        var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

        var request = HttpContext.Request;

        if (!request.HasFormContentType ||
            !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
            string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
        {
            return BadRequest("Unsupported media type.");
        }

        var formCollection = await request.ReadFormAsync();

        var file = formCollection?.Files?[0];

        if (file is not null && file.Length > 0)
        {
            var uploadResponse = await _photoUploadService.UploadPhoto(file);

            if (uploadResponse.IsSuccess == false)
            {
                return BadRequest(uploadResponse.Errors);
            }

            var photo = new Photo
            {
                Name = uploadResponse.Name,
                DisplayName = _htmlEncoder.Encode(file.FileName),
                URL = uploadResponse.PhotoURI,
                UserId = currentUser.Id,
            };

            var result = await _photoService.AddPhotoAsync(photo);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            var photoResponseDto = new PhotoResponseDto
            {
                Name = photo.Name,
                DisplayName = photo.DisplayName,
                URL = photo.URL
            };

            return Ok(photoResponseDto);
        }

        return BadRequest();
    }
}
