using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
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
    private readonly IPhotoService _photoService;
    private readonly IPhotoUploadService _photoUploadService;

    public PhotosController(UserManager<ApplicationUser> userManager,
                            IPhotoService photoService,
                            IPhotoUploadService photoUploadService)
    {
        _userManager = userManager;
        _photoService = photoService;
        _photoUploadService = photoUploadService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
                URL = uploadResponse.PhotoURI,
                UserId = currentUser.Id,
            };

            var result = await _photoService.AddPhoto(photo);

            if (!result.IsSuccess) return BadRequest(result.Errors);

            return Ok();
        }

        return BadRequest();
    }
}
