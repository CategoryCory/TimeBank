using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class UserSkillsDto
    {
        [Required(ErrorMessage = "Skill ID is required.")]
        public Guid UserSkillId { get; set; }

        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(50, ErrorMessage = "Skill name cannot exceed 50 characters.")]
        public string SkillName { get; set; }

        public string SkillNameSlug { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
    }
}
