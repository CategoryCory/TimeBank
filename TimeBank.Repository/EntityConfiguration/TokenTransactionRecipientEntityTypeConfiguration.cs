using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class TokenTransactionRecipientEntityTypeConfiguration : IEntityTypeConfiguration<TokenTransactionRecipient>
    {
        public void Configure(EntityTypeBuilder<TokenTransactionRecipient> builder)
        {
            builder.HasOne(r => r.User)
                .WithMany(u => u.TokenTransactionRecipients);
        }
    }
}
