using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class TokenTransactionRecipient
    {
        public int TokenTransactionRecipientId { get; set; }

        public int TokenTransactionId { get; set; }
        public TokenTransaction TokenTransaction { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
