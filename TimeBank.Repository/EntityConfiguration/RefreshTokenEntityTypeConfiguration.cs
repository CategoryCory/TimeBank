using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(t => t.Token)
            .IsRequired()
            .HasMaxLength(64);
        builder.Property(t => t.CreatedOn)
            .IsRequired();
        builder.Property(t => t.ExpiresOn)
            .IsRequired();

        //builder.HasOne(t => t.User)
        //    .WithMany(u => u.RefreshTokens);
    }
}
