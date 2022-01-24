using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class TokenTransaction
    {
        public int TokenTransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessedOn { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
    }
}
