using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }


        public DbSet<SubscriptionLevel> SubscriptionLevels { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<TemplateHistory> TemplateHistory { get; set; }
    }
}
