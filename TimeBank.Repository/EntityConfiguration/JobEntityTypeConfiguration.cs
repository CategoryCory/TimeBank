using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TimeBank.Entities.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(j => j.Description)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(j => j.JobStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(JobStatus.Available);
            builder.Property(j => j.ExpiresOn)
                .HasColumnType("date");
            builder.Property(j => j.CreatedOn)
                .HasColumnType("date")
                .HasDefaultValueSql("getdate()");
        }
    }
}
