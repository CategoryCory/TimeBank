using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record JobCategoryDto
    {
        [Required]
        [MaxLength(150)]
        public string JobCategoryName { get; init; }
    }
}
