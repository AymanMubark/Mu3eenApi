using Microsoft.EntityFrameworkCore;
using Mu3een.Entities;

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
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<SocialServiceType> SocialServiceTypes { get; set; }
        public DbSet<SocialService> SocialServices { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<VolunteerService> VolunteerServices { get; set; }
        public DbSet<VolunteerReward> VolunteerRewards { get; set; }
    }
}
