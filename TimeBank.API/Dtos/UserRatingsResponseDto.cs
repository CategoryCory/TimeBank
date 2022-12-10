using System;

namespace TimeBank.API.Dtos
{
    public record UserRatingsResponseDto
    {
        public int UserRatingId { get; init; }
        public double Rating { get; init; }
        public string Comments { get; init; }
        public DateTime CreatedOn { get; init; }
        public string AuthorId { get; init; }
        public string AuthorName { get; init; }
        public string RevieweeId { get; init; }
        public string RevieweeName { get; init; }
    }
}
