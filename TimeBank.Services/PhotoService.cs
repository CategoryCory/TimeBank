using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services;

public class PhotoService : IPhotoService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PhotoService> _logger;
    private readonly PhotoValidator _validator;

    public PhotoService(ApplicationDbContext context, ILogger<PhotoService> logger)
    {
        _context = context;
        _logger = logger;
        _validator = new PhotoValidator();
    }

    public async Task<ApplicationResult> AddPhoto(Photo photo)
    {
        ValidationResult result = _validator.Validate(photo);

        if (!result.IsValid)
        {
            _logger.LogError("Failed to upload photo.");

            return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
        }

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        return ApplicationResult.Success();
    }
}
