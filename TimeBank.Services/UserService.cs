using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;

namespace TimeBank.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager,
                           ApplicationDbContext context,
                           ILogger<UserService> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<ApplicationResult> SetUserSkillsAsync(ApplicationUser user, List<UserSkill> skillsToSet)
        {
            // Load skills to set from db
            var skillsIds = skillsToSet.Select(s => s.UserSkillId);
            var skillsFromDb = await _context.UserSkills.Where(s => skillsIds.Contains(s.UserSkillId)).ToListAsync();

            // Check user's skills against incoming skills and remove as necessary
            foreach (var userSkill in user.Skills)
            {
                if (!skillsFromDb.Contains(userSkill)) user.Skills.Remove(userSkill);
            }

            // Check the list of incoming skills and add to user's skills as necessary
            foreach (var skillToSet in skillsFromDb)
            {
                if (!user.Skills.Contains(skillToSet)) user.Skills.Add(skillToSet);
            }

            await _userManager.UpdateAsync(user);

            return ApplicationResult.Success();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(bool showOnlyUnapproved = false)
        {
            var users = _userManager.Users.AsNoTracking();

            if (showOnlyUnapproved)
            {
                users = users.Where(u => u.IsApproved);
            }

            return await users.ToListAsync();
        }
    }
}
