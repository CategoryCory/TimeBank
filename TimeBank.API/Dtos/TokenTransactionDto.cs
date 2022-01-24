namespace TimeBank.API.Dtos
{
    public class TokenTransactionDto
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public double Amount { get; set; }
    }
}
