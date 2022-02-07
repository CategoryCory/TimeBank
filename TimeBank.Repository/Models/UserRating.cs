using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class UserRating
    {
        public int UserRatingId { get; set; }
        public double Rating { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public string RevieweeId { get; set; }
        public ApplicationUser Reviewee { get; set; }
    }
}
