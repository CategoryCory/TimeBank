using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class JobApplicationEntityTypeConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(JobApplicationStatus.Pending);
            builder.Property(r => r.CreatedOn)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            builder.HasOne(r => r.Job)
                .WithMany(j => j.JobApplications);

            builder.HasOne(r => r.Applicant)
                .WithMany(u => u.JobApplications);

            builder.HasMany(r => r.JobSchedules)
                .WithMany(s => s.JobApplications)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationSchedule",
                    r => r
                         .HasOne<JobSchedule>()
                         .WithMany()
                         .OnDelete(DeleteBehavior.NoAction),
                    s => s
                         .HasOne<JobApplication>()
                         .WithMany()
                         .OnDelete(DeleteBehavior.NoAction)
                );
        }
    }
}
