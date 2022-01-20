using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class TokenTransactionEntityTypeConfiguration : IEntityTypeConfiguration<TokenTransaction>
    {
        public void Configure(EntityTypeBuilder<TokenTransaction> builder)
        {
            builder.Property(t => t.Amount)
                .IsRequired()
                .HasDefaultValue(0.0);
            builder.Property(t => t.ProcessedOn)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.HasOne(t => t.User)
                .WithMany(u => u.TokenTransactions);

            builder.HasOne(t => t.Recipient)
                .WithOne(r => r.TokenTransaction)
                .HasForeignKey<TokenTransactionRecipient>(r => r.TokenTransactionId);
        }
    }
}
