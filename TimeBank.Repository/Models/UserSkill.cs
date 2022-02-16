using System;
using System.Collections.Generic;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class UserSkill
    {
        public Guid UserSkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillNameSlug { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
