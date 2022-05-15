using System;

namespace TimeBank.API.Dtos
{
    public class MessageThreadResponseDto
    {
        public int MessageThreadId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int JobId { get; set; }
        public string ToUserId { get; set; }
        public string FromUserId { get; set; }
    }
}
