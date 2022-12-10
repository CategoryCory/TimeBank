using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services;

public sealed class PhotoService : IPhotoService
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

    public async Task<Photo> GetCurrentPhotoByUserIdAsync(string userId)
    {
        return await _context.Photos.AsNoTracking()
                                    .Where(p => p.UserId == userId && p.IsCurrent == true)
                                    .FirstOrDefaultAsync();
    }

    public async Task<ApplicationResult> AddPhotoAsync(Photo photoToAdd)
    {
        ValidationResult result = _validator.Validate(photoToAdd);

        if (!result.IsValid)
        {
            _logger.LogError("Failed to upload photo.");

            return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
        }

        // Check to see if another photo is current; if so, make this one current
        var currentPhotos = await _context.Photos.Where(p => p.UserId == photoToAdd.UserId && p.IsCurrent == true).ToListAsync();

        foreach (var p in currentPhotos)
        {
            p.IsCurrent = false;
        }

        photoToAdd.IsCurrent = true;

        _context.Photos.Add(photoToAdd);
        await _context.SaveChangesAsync();

        return ApplicationResult.Success();
    }
}
