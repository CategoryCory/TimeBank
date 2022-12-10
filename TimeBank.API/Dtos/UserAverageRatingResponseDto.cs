namespace TimeBank.API.Dtos
{
    public record UserAverageRatingResponseDto
    {
        public string UserId { get; init; }
        public double AverageRating { get; init; }
    }
}
