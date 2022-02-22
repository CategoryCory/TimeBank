﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class JobScheduleEntityTypeConfiguration : IEntityTypeConfiguration<JobSchedule>
    {
        public void Configure(EntityTypeBuilder<JobSchedule> builder)
        {
            builder.Property(s => s.DayOfWeek)
                .IsRequired();
            builder.Property(s => s.TimeBegin)
                .IsRequired();
            builder.Property(s => s.TimeEnd)
                .IsRequired();

            builder.HasOne(s => s.Job)
                .WithMany(j => j.JobSchedules);
        }
    }
}
