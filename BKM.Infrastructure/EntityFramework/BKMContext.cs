
using Microsoft.EntityFrameworkCore;

namespace BKM.Infrastructure.EntityFramework
{
    public class BKMContext : DbContext
    {
        public BKMContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}


