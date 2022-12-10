using System;

namespace TimeBank.API.Dtos
{
    public record TokenTransactionResponseDto
    {
        public int TokenTransactionId { get; init; }
        public string SenderId { get; init; }
        public string SenderName { get; init; }
        public string RecipientId { get; init; }
        public string RecipientName { get; init; }
        public double Amount { get; init; }
        public DateTime ProcessedOn { get; init; }
    }
}
