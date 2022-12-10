using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record JobApplicationStatusUpdateDto
    {
        [Required(ErrorMessage = "The job application id is missing.")]
        public int JobApplicationId { get; init; }

        [Required(ErrorMessage = "The job application status is missing.")]
        public string Status { get; init; }
    }
}
