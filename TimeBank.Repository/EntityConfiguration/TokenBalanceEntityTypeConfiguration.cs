using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class TokenBalanceEntityTypeConfiguration : IEntityTypeConfiguration<TokenBalance>
    {
        public void Configure(EntityTypeBuilder<TokenBalance> builder)
        {
            builder.Property(t => t.CurrentBalance)
                .IsRequired()
                .HasDefaultValue(5.0);
        }
    }
}
