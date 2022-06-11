using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration;

public class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(200);
        builder.Property(p => p.URL)
            .HasMaxLength(450);
        builder.HasOne(p => p.User)
            .WithMany(u => u.Photos);
    }
}
