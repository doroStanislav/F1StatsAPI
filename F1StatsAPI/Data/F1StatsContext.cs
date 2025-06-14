using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Models;

namespace F1StatsAPI.Data
{
    public class F1StatsContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<GrandPrix> GrandPrix { get; set; }
    }
}
