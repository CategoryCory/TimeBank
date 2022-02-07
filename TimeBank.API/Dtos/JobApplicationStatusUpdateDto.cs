using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobApplicationStatusUpdateDto
    {
        [Required(ErrorMessage = "The job application id is missing.")]
        public int JobApplicationId { get; set; }

        [Required(ErrorMessage = "The job application status is missing.")]
        public string Status { get; set; }
    }
}
