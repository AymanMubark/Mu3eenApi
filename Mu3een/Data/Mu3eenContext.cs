using Microsoft.EntityFrameworkCore;

namespace Mu3een.Data
{
    public class Mu3eenContext : DbContext
    {
        public Mu3eenContext(DbContextOptions<Mu3eenContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
 

    }
}
