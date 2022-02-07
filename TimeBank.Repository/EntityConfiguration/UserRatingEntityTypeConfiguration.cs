using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class UserRatingEntityTypeConfiguration : IEntityTypeConfiguration<UserRating>
    {
        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            builder.Property(r => r.Rating)
                .IsRequired();
            builder.Property(r => r.Comments)
                .HasMaxLength(200);
            builder.Property(r => r.CreatedOn)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            builder.HasOne(r => r.Author)
                .WithMany(u => u.AuthoredRatings);

            builder.HasOne(r => r.Reviewee)
                .WithMany(u => u.ReceivedRatings);
        }
    }
}
