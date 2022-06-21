using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;
using TimeBank.Services.Comparers;
using TimeBank.Services.Contracts;
using TimeBank.Services.Extensions;

namespace TimeBank.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly SkillComparer _skillComparer;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _skillComparer = new SkillComparer();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(bool showOnlyUnapproved = false)
        {
            var users = _context.Users.AsNoTracking();

            if (showOnlyUnapproved)
            {
                users = users.Where(u => u.IsApproved);
            }

            return await users.Include(u => u.Skills.Where(s => s.IsCurrent == true))
                              .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                              .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Skills)
                                           .Include(u => u.Photos.Where(p => p.IsCurrent == true))
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<ApplicationResult> UpdateUserAsync(ApplicationUser userToUpdate)
        {
            var userFromDb = await _context.Users.Where(u => u.Id == userToUpdate.Id).SingleOrDefaultAsync();

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

                // Adjust skills
                // Get current skills for user
                var currentSkills = await _context.UserSkills.Where(s => s.UserId == userToUpdate.Id).ToListAsync();

                var skillsToAdd = userToUpdate.Skills.Except(currentSkills, _skillComparer).ToList();
                var skillsToRemove = currentSkills.Except(userToUpdate.Skills, _skillComparer).ToList();

                foreach (var skill in skillsToAdd)
                {
                    skill.IsCurrent = true;
                    skill.UserId = userToUpdate.Id;

                    _context.UserSkills.Add(skill);
                }

                foreach (var skill in skillsToRemove)
                {
                    _context.UserSkills.Remove(skill);
                }

                await _context.SaveChangesAsync();

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
