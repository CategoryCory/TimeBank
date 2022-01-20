using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Biography { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public bool IsApproved { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public TokenBalance TokenBalance { get; set; }

        public ICollection<TokenTransaction> TokenTransactions { get; set; }

        public ICollection<TokenTransactionRecipient> TokenTransactionRecipients { get; set; }
    }
}
