using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class UserRatingsDto
    {
        [Required(ErrorMessage = "The rating field is required.")]
        [Range(1.0, 5.0, ErrorMessage = "The rating must be between 1 and 5.")]
        public double Rating { get; set; }

        [MaxLength(200, ErrorMessage = "The comments cannot be longer than 200 characters.")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "The author ID field is required.")]
        public string AuthorId { get; set; }

        [Required(ErrorMessage = "The reviewee ID field is required.")]
        public string RevieweeId { get; set; }
    }
}
