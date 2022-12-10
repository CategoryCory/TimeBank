using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record UserSkillsDto
    {
        [Required(ErrorMessage = "Skill ID is required.")]
        public Guid UserSkillId { get; init; }

        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(50, ErrorMessage = "Skill name cannot exceed 50 characters.")]
        public string SkillName { get; init; }

        public string SkillNameSlug { get; init; }
        public bool IsCurrent { get; init; }
        public DateTime CreatedOn { get; init; }
        public string UserId { get; init; }
    }
}
