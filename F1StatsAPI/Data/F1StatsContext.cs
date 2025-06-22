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

            modelBuilder.Entity<Driver>().HasData(
                new Driver
                {
                    Id = 1,
                    Code = "LEC",
                    Number = 16,
                    GivenName = "Charles",
                    FamilyName = "Leclerc",
                    Country = "Monaco",
                    DateOfBirth = new DateTime(1997, 10, 16),
                    TeamId = 1,
                },

                new Driver
                {
                    Id = 2,
                    Code = "HAM",
                    Number = 44,
                    GivenName = "Lewis",
                    FamilyName = "Hamilton",
                    Country = "United Kingdom",
                    DateOfBirth = new DateTime(1985, 1, 7),
                    TeamId = 1,
                }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Scuderia Ferrari",
                    TeamChief = "Frédéric Vasseur",
                    WorldChampionships = 16,
                    BaseLocation = "Maranello, Italy",
                    FoundationYear = 1929,
                    CarId = 1,
                }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Name = "Ferrari SF-25",
                    ChassisCode = "SF-25",
                    PoweUnit = "Ferrari"
                }
            );

            modelBuilder.Entity<GrandPrix>().HasData(
                new GrandPrix
                {
                    Id = 1,
                    Name = "Australian Grand Prix",
                    CircuitName = "Albert Park Circuit",
                    Date = new DateTime(2025, 3, 16),
                    Laps = 58,
                    Distance = 306.124
                }
            );

            modelBuilder.Entity<Result>().HasData(
                new Result
                {
                    Id = 1,
                    GrandPrixId = 1,
                    DriverId = 1,
                    TeamId = 1,
                    CarId = 1,
                    Position = 8,
                    Points = 4,
                    Time = null,
                    GapToLeader = "+19.826s",
                    DidNotFinish = false
                },

                new Result
                {
                    Id = 2,
                    GrandPrixId = 1,
                    DriverId = 2,
                    TeamId = 1,
                    CarId = 1,
                    Position = 10,
                    Points = 1,
                    Time = null,
                    GapToLeader = "+22.473s",
                    DidNotFinish = false
                }
            );
        }
    }
}
