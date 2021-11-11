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
            builder.Property(j => j.DisplayId)
                .IsRequired()
                .HasMaxLength(36)
                .IsFixedLength();
            builder.Property(j => j.JobName)
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
                .IsRequired()
                .HasColumnType("date");
            builder.Property(j => j.CreatedOn)
                .HasColumnType("date")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");
        }
    }
}
