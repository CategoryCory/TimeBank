using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record UserRatingsDto
    {
        [Required(ErrorMessage = "The rating field is required.")]
        [Range(0.5, 5.0, ErrorMessage = "The rating must be between 0.5 and 5.")]
        public double Rating { get; init; }

        [MaxLength(200, ErrorMessage = "The comments cannot be longer than 200 characters.")]
        public string Comments { get; init; }

        [Required(ErrorMessage = "The author ID field is required.")]
        public string AuthorId { get; init; }

        [Required(ErrorMessage = "The reviewee ID field is required.")]
        public string RevieweeId { get; init; }
    }
}
