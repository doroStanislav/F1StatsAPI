using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Models;

namespace F1StatsAPI.Data
{
    public class F1StatsContext : DbContext
    {
        public F1StatsContext(DbContextOptions<F1StatsContext> options) : base(options) { }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<GrandPrix> GrandPrix { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Team)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasOne(d => d.Car)
                .WithOne()
                .HasForeignKey<Team>(t => t.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
