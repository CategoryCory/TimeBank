using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; private set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime Birthday { get; set; }
        public string Biography { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public bool IsApproved { get; set; }

        public TokenBalance TokenBalance { get; set; }

        public ICollection<Job> Jobs { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<TokenTransaction> SentTransactions { get; set; }
        public ICollection<TokenTransaction> ReceivedTransactions { get; set; }
        public ICollection<UserRating> AuthoredRatings { get; set; }
        public ICollection<UserRating> ReceivedRatings { get; set; }
        public ICollection<UserSkill> Skills { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
