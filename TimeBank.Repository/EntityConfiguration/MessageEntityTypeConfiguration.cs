using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeBank.Repository.Models;

namespace TimeBank.Repository.EntityConfiguration
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.Body)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(m => m.CreatedOn)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(m => m.MessageThread)
                .WithMany(t => t.Messages);

            builder.HasOne(m => m.Author)
                .WithMany();
        }
    }
}
