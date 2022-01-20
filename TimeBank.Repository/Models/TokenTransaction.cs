using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class TokenTransaction
    {
        public int TokenTransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessedOn { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TokenTransactionRecipient Recipient { get; set; }
    }
}
