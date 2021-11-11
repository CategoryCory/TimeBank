using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.EntityConfiguration
{
    internal class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.StreetAddress)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(a => a.ZipCode)
                .IsRequired()
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
