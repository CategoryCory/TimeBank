using System;

namespace TimeBank.API.Dtos
{
    public record MessageThreadResponseDto
    {
        public int MessageThreadId { get; init; }
        public DateTime CreatedOn { get; init; }
        public int JobId { get; init; }
        public string ToUserId { get; init; }
        public string FromUserId { get; init; }
    }
}
