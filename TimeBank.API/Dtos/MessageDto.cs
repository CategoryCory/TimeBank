using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class MessageDto
    {
        public int MessageId { get; set; }

        [Required]
        public bool IsFromSender { get; set; }

        [Required]
        [MaxLength(500)]
        public string Body { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadOn { get; set; }

        [Required]
        public int MessageThreadId { get; set; }

        [Required]
        public string AuthorId { get; set; }
    }
}
