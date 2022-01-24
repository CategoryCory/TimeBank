using System;

namespace TimeBank.API.Dtos
{
    public class TokenTransactionResponseDto
    {
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime ProcessedOn { get; set; }
        public string ActionDescription { get; set; }
    }
}
