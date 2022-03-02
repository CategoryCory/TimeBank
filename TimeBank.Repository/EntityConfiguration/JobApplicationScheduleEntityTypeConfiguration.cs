using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class JobApplicationScheduleEntityTypeConfiguration : IEntityTypeConfiguration<JobApplicationSchedule>
    {
        public void Configure(EntityTypeBuilder<JobApplicationSchedule> builder)
        {
            builder.HasOne(j => j.JobSchedule)
                .WithMany(s => s.JobApplicationSchedules)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(j => j.JobApplication)
                .WithMany(a => a.JobApplicationSchedules)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(j => new { j.JobScheduleId, j.JobApplicationId });
        }
    }
}
