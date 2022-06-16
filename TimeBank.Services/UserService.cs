using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository.IdentityModels;
using TimeBank.Services.Comparers;
using TimeBank.Services.Contracts;

namespace TimeBank.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserService> _logger;
        private readonly SkillComparer _skillComparer;

        public UserService(UserManager<ApplicationUser> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _skillComparer = new SkillComparer();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(bool showOnlyUnapproved = false)
        {
            var users = _userManager.Users.AsNoTracking();

            if (showOnlyUnapproved)
            {
                users = users.Where(u => u.IsApproved);
            }

            return await users.Include(u => u.Skills)
                              .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                              .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.Users.Include(u => u.Skills)
                                           .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                                           .SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationResult> UpdateUserAsync(ApplicationUser userToUpdate)
        {
            // We need this to 1) check if user exists, and 2) compare skills
            var userFromDb = await _userManager.Users.Where(u => u.Id == userToUpdate.Id)
                                                     .Include(u => u.Skills)
                                                     .SingleOrDefaultAsync();

            if (userFromDb is null)
            {
                _logger.LogError("User {userId} could not be updated: the user was not found.", userToUpdate.Id);
                return ApplicationResult.Failure(new List<string> { $"User {userToUpdate.Id} could not be found" });
            }

            try
            {
                // Copy other fields to userToUpdate
                userFromDb.FirstName = userToUpdate.FirstName;
                userFromDb.LastName = userToUpdate.LastName;
                userFromDb.PhoneNumber = userToUpdate.PhoneNumber;
                userFromDb.StreetAddress = userToUpdate.StreetAddress;
                userFromDb.City = userToUpdate.City;
                userFromDb.State = userToUpdate.State;
                userFromDb.ZipCode = userToUpdate.ZipCode;
                userFromDb.Birthday = userToUpdate.Birthday;
                userFromDb.Biography = userToUpdate.Biography;
                userFromDb.Facebook = userToUpdate.Facebook;
                userFromDb.Twitter = userToUpdate.Twitter;
                userFromDb.Instagram = userToUpdate.Instagram;
                userFromDb.LinkedIn = userToUpdate.LinkedIn;

                // Adjust skill list
                var skillsToAdd = userToUpdate.Skills.Except(userFromDb.Skills, _skillComparer).ToList();
                var skillsToRemove = userFromDb.Skills.Except(userToUpdate.Skills, _skillComparer).ToList();

                foreach (var skill in skillsToAdd)
                {
                    userFromDb.Skills.Add(skill);
                }

                foreach (var skill in skillsToRemove)
                {
                    userFromDb.Skills.Remove(skill);
                }

                // Save user changes
                await _userManager.UpdateAsync(userFromDb);

                return ApplicationResult.Success();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("An error occurred updating the user: {message}", ex.InnerException.Message);
                throw new Exception(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred updating the user: {message}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
