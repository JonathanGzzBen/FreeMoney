using Microsoft.EntityFrameworkCore;
using FreeMoney.Web.Models;

namespace FreeMoney.Web.Data
{

    public class FreeMoneyDbContext : DbContext
    {
        public FreeMoneyDbContext(DbContextOptions<FreeMoneyDbContext> options) : base(options)
        {

        }

        public DbSet<UserRecord> UserRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRecord>().ToTable("UserRecord");
        }
    }
}