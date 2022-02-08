using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class UserRatingService : IUserRatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRatingService> _logger;
        private readonly UserRatingValidator _userRatingValidator;

        public UserRatingService(ApplicationDbContext context, ILogger<UserRatingService> logger)
        {
            _context = context;
            _logger = logger;
            _userRatingValidator = new UserRatingValidator();
        }

        public async Task<List<UserRating>> GetAllReceivedRatingsByUserIdAsync(string userId)
        {
            return await _context.UserRatings.AsNoTracking()
                .Where(r => r.RevieweeId == userId)
                .Include(r => r.Author)
                .Include(r => r.Reviewee)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingByUserIdAsync(string userId)
        {
            return await _context.UserRatings.Where(r => r.RevieweeId == userId).AverageAsync(r => r.Rating);
        }

        public async Task<ApplicationResult> AddRatingAsync(UserRating userRating)
        {
            ValidationResult result = _userRatingValidator.Validate(userRating);

            if (!result.IsValid)
            {
                _logger.LogError("The rating for user {} could not be created.", userRating.RevieweeId);

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            _context.UserRatings.Add(userRating);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
