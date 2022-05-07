using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class MessageThreadDto
    {
        [Required(ErrorMessage = "The job id is required")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "The recipient user id is required")]
        public string ToUserId { get; set; }

        [Required(ErrorMessage = "The sender user id is required")]
        public string FromUserId { get; set; }
    }
}
