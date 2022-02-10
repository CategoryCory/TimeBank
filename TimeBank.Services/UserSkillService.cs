using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimeBank.Repository;
using TimeBank.Repository.Models;
using TimeBank.Services.Contracts;
using TimeBank.Services.Extensions;
using TimeBank.Services.Validators;

namespace TimeBank.Services
{
    public class UserSkillService : IUserSkillService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserSkillService> _logger;
        private readonly UserSkillValidator _validator;

        public UserSkillService(ApplicationDbContext context, ILogger<UserSkillService> logger)
        {
            _context = context;
            _logger = logger;
            _validator = new UserSkillValidator();
        }

        public async Task<List<UserSkill>> GetSkillsAsync(string searchString)
        {
            var skills = _context.UserSkills.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchString))
                skills = skills.Where(s => s.SkillName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));

            return await skills.ToListAsync();
        }

        public async Task<ApplicationResult> AddSkillAsync(UserSkill userSkill)
        {
            ValidationResult result = _validator.Validate(userSkill);

            if (!result.IsValid)
            {
                _logger.LogError("Could not create skill with name {}", userSkill?.SkillName);

                return ApplicationResult.Failure(result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            userSkill.SkillNameSlug = userSkill.SkillName.Slugify();
            _context.UserSkills.Add(userSkill);
            await _context.SaveChangesAsync();

            return ApplicationResult.Success();
        }
    }
}
