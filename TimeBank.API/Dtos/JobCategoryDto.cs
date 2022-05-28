using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobCategoryDto
    {
        [Required]
        [MaxLength(150)]
        public string JobCategoryName { get; set; }
    }
}
