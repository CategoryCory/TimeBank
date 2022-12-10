using System;

namespace TimeBank.API.Dtos
{
    public record MessageResponseDto
    {
        public int MessageId { get; init; }
        public bool IsFromSender { get; init; }
        public DateTime CreatedOn { get; init; }
        public bool IsRead { get; init; }
        public DateTime? ReadOn { get; init; }
        public string Body { get; init; }
        public string AuthorId { get; init; }
        public string RecipientId { get; init; }
        public int MessageThreadId { get; init; }
        public int JobId { get; init; }
    }
}
