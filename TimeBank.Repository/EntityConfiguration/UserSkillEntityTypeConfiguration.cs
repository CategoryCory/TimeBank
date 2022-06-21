using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class UserSkillEntityTypeConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.Property(s => s.SkillName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(s => s.SkillNameSlug)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(s => s.CreatedOn)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            builder.HasIndex(s => s.SkillNameSlug)
                .IsUnique();

            builder.HasOne(s => s.User)
                .WithMany(u => u.Skills);
        }
    }
}
