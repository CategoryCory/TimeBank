namespace TimeBank.API.Dtos
{
    public record TokenBalanceResponseDto
    {
        public string UserId { get; init; }
        public double CurrentBalance { get; init; }
    }
}
