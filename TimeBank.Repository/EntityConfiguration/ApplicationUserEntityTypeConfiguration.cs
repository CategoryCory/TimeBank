using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.EntityConfiguration
{
    internal class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.StreetAddress)
                .HasMaxLength(100);
            builder.Property(a => a.City)
                .HasMaxLength(50);
            builder.Property(a => a.State)
                .HasMaxLength(25);
            builder.Property(a => a.ZipCode)
                .HasMaxLength(25);
            builder.Property(a => a.Biography)
                .HasMaxLength(500);
            builder.Property(a => a.Facebook)
                .HasMaxLength(200);
            builder.Property(a => a.Twitter)
                .HasMaxLength(200);
            builder.Property(a => a.Instagram)
                .HasMaxLength(200);
            builder.Property(a => a.LinkedIn)
                .HasMaxLength(200);
            builder.Property(a => a.IsApproved)
                .HasDefaultValue(false);
        }
    }
}
