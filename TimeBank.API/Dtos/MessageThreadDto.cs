using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record MessageThreadDto
    {
        [Required(ErrorMessage = "The job id is required")]
        public int JobId { get; init; }

        [Required(ErrorMessage = "The recipient user id is required")]
        public string ToUserId { get; init; }

        [Required(ErrorMessage = "The sender user id is required")]
        public string FromUserId { get; init; }
    }
}
