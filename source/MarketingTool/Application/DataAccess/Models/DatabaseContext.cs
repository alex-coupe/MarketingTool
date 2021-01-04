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
    }
}
