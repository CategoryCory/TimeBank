using System;

namespace TimeBank.API.Dtos
{
    public class MessageResponseDto
    {
        public int MessageId { get; set; }
        public bool IsFromSender { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadOn { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
        public string RecipientId { get; set; }
        public int MessageThreadId { get; set; }
        public int JobId { get; set; }
    }
}
