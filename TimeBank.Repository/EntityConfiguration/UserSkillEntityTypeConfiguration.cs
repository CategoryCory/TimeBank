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

            builder.HasMany(s => s.Users)
                .WithMany(u => u.Skills);
        }
    }
}
