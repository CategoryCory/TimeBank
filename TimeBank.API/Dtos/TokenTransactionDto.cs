namespace TimeBank.API.Dtos
{
    public record TokenTransactionDto
    {
        public string SenderId { get; init; }
        public string RecipientId { get; init; }
        public double Amount { get; init; }
    }
}
