using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class JobCategoryEntityTypeConfiguration : IEntityTypeConfiguration<JobCategory>
    {
        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.Property(c => c.JobCategoryName)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(c => c.JobCategorySlug)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
