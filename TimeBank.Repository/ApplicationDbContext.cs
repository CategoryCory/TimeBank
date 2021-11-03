using Microsoft.EntityFrameworkCore;
using TimeBank.Entities.Models;
using TimeBank.Repository.EntityConfiguration;

namespace TimeBank.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobEntityTypeConfiguration).Assembly);
        }

        public DbSet<Job> Jobs { get; set; }
    }
}
