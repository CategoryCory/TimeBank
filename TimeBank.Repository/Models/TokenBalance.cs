using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class TokenBalance
    {
        public int TokenBalanceId { get; set; }
        public double CurrentBalance { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
