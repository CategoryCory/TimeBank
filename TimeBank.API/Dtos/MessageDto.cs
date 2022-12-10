using System;
using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record MessageDto
    {
        public int MessageId { get; init; }

        [Required]
        public bool IsFromSender { get; init; }

        [Required]
        [MaxLength(500)]
        public string Body { get; init; }

        public DateTime CreatedOn { get; init; }
        public bool IsRead { get; init; }
        public DateTime? ReadOn { get; init; }

        [Required]
        public int MessageThreadId { get; init; }

        [Required]
        public string AuthorId { get; init; }
    }
}
