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

            builder.Entity<EmailStatus>().HasData(
                new EmailStatus { Id = 1, Name = "Pending" }, 
                new EmailStatus { Id = 2, Name = "Sent" }, 
                new EmailStatus { Id = 3, Name = "Failed" });

            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Root", Description = "Allows access to all API routes and all clients/users, for staff use only" },
                new Role { Id = 2, Name = "Founder", Description = "Admin privileges but shows who registered the client" }, 
                new Role { Id = 3, Name = "Admin", Description = "Admin privileges" },
                new Role { Id = 4, Name = "User", Description = "Standard user who can have permissions edited by Admin/Founder" });

            builder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Global", Description = "All permissions are switched on" },
                new Permission { Id = 2, Name = "Import Recipients", Description = "Allow user to import new recipients" },
                new Permission { Id = 3, Name = "Edit Schema", Description = "Allow user to edit the recipient schema" },
                new Permission { Id = 4, Name = "Add Campaigns", Description = "User can create new campaigns" },
                new Permission { Id = 5, Name = "Edit Campaigns", Description = "User can edit existing campaigns" },
                new Permission { Id = 6, Name = "Add Templates", Description = "User can create new templates" },
                new Permission { Id = 7, Name = "Edit Templates", Description = "User can edit existing templates" },
                new Permission { Id = 8, Name = "Add Lists", Description = "User can create new lists" },
                new Permission { Id = 9, Name = "Edit Lists", Description = "User can edit existing lists" },
                new Permission { Id = 10, Name = "Add Recipients", Description = "User can add new recipients" },
                new Permission { Id = 11, Name = "Edit Recipients", Description = "User can edit existing recipients" },
                new Permission { Id = 12, Name = "Add Template Synonyms", Description = "User can create new template synonyms" },
                new Permission { Id = 13, Name = "Edit Template Synonyms", Description = "User can edit existing template synonyms" }
                );
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<CampaignJobHistory> CampaignJobHistory { get; set; }

        public DbSet<UserInvite> UserInvites { get; set; }

        public DbSet<TemplateSynonym> TemplateSynonyms { get; set; }

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

        public DbSet<ListRecipient> ListRecipients { get; set; }
    }
}
