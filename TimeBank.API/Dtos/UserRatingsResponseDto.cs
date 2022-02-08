using System;

namespace TimeBank.API.Dtos
{
    public class UserRatingsResponseDto
    {
        public double Rating { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string RevieweeId { get; set; }
        public string RevieweeName { get; set; }
    }
}
