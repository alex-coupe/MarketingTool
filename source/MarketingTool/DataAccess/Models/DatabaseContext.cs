using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TimeStep>().HasData(new TimeStep { Id = 1, Name = "ASAP", Hours = 0 }, new TimeStep { Id = 2, Name = "Hourly", Hours = 1 }, new TimeStep { Id = 3, Name = "Daily", Hours = 24 }
            , new TimeStep { Id = 4, Name = "Weekly", Hours = 168 }, new TimeStep { Id = 5, Name = "Bi-Weekly", Hours = 336 }, new TimeStep { Id = 6, Name = "4 Weekly", Hours = 672 }, new TimeStep
            {
                Id = 7,
                Name = "Monthly",
                Hours = 730
            });
        }


        public DbSet<TimeStep> TimeSteps { get; set; }
        public DbSet<SubscriptionLevel> SubscriptionLevels { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<TemplateHistory> TemplateHistory { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
    }
}
