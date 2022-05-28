using System;

namespace TimeBank.API.Dtos
{
    public class TokenTransactionResponseDto
    {
        public int TokenTransactionId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessedOn { get; set; }
    }
}
