using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class UserSkillsDto
    {
        public Guid UserSkillId { get; set; }

        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(50, ErrorMessage = "Skill name cannot exceed 50 characters.")]
        public string SkillName { get; set; }

        public string SkillNameSlug { get; set; }
    }
}
