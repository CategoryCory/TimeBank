using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record JobApplicationDto
    {
        [Required(ErrorMessage = "The job id is missing.")]
        public int JobId { get; init; }

        public string ApplicantId { get; init; }

        public int JobApplicationScheduleId { get; init; }
    }
}
