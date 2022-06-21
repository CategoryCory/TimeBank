using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class UserSkill
    {
        public Guid UserSkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillNameSlug { get; set; }
        public bool IsCurrent { get; set; } = true;
        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
