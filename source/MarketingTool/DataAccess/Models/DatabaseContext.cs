using DataAccess.Configurations;
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
            builder.ApplyConfiguration(new RecipientSchemaConfiguration());
            builder.ApplyConfiguration(new RecipientConfiguration());

            builder.Entity<Timestep>().HasData(new Timestep { Id = 1, Name = "ASAP", Hours = 0 }, new Timestep { Id = 2, Name = "Hourly", Hours = 1 }, new Timestep { Id = 3, Name = "Daily", Hours = 24 }
            , new Timestep { Id = 4, Name = "Weekly", Hours = 168 }, new Timestep { Id = 5, Name = "Bi-Weekly", Hours = 336 }, new Timestep { Id = 6, Name = "4 Weekly", Hours = 672 }, new Timestep
            {
                Id = 7,
                Name = "Monthly",
                Hours = 730
            });

            builder.Entity<EmailStatus>().HasData(new EmailStatus { Id = 1, Name = "Pending" }, new EmailStatus { Id = 2, Name = "Sent" }, new EmailStatus { Id = 3, Name = "Failed" });
        }

        public DbSet<CampaignJob> CampaignJobs { get; set; }

        public DbSet<CampaignJobHistory> CampaignJobHistory { get; set; }

        public DbSet<UserInvite> UserInvites { get; set; }

        public DbSet<TemplateSynonym> TemplateSynonyms { get; set; }

        public DbSet<Timestep> Timesteps { get; set; }

        public DbSet<EmailStatus> EmailStatuses { get; set; }
        public DbSet<SubscriptionLevel> SubscriptionLevels { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<RecipientSchema> RecipientSchemas { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<TemplateHistory> TemplateHistory { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
    }
}
