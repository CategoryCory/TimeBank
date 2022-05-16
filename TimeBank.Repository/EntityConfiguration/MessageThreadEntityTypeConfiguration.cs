using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class MessageThreadEntityTypeConfiguration : IEntityTypeConfiguration<MessageThread>
    {
        public void Configure(EntityTypeBuilder<MessageThread> builder)
        {
            builder.Property(t => t.CreatedOn)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            builder.HasIndex(t => new
            {
                t.JobId,
                t.ToUserId,
                t.FromUserId
            })
                .IsUnique();
        }
    }
}
