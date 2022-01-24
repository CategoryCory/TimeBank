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

            builder.HasOne(t => t.Sender)
                .WithMany(u => u.SentTransactions)
                .HasForeignKey(t => t.SenderId);

            builder.HasOne(t => t.Recipient)
                .WithMany(u => u.ReceivedTransactions)
                .HasForeignKey(t => t.RecipientId);
        }
    }
}
