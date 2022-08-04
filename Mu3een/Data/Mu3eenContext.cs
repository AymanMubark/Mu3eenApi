using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mu3een.Entities;

namespace Mu3een.Data
{
    public class Mu3eenContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public Mu3eenContext(DbContextOptions<Mu3eenContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<SocialEvent>()
                .HasIndex(p => new { p.Name, p.Description });
            
        }

        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Volunteer> Volunteers { get; set; } = null!;
        public DbSet<Institution> Institutions { get; set; } = null!;
        public DbSet<SocialEventType> SocialEventTypes { get; set; } = null!;
        public DbSet<SocialEvent> SocialEvents { get; set; } = null!;
        public DbSet<Reward> Rewards { get; set; } = null!;
        public DbSet<SocialEventVolunteer> SocialEventVolunteers { get; set; } = null!;
        public DbSet<VolunteerReward> VolunteerRewards { get; set; } = null!;
    }
}
