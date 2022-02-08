using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeBank.Repository.EntityConfiguration;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;

namespace TimeBank.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<TokenBalance> TokenBalances { get; set; }
        public DbSet<TokenTransaction> TokenTransactions { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobEntityTypeConfiguration).Assembly);
        }
    }
}
