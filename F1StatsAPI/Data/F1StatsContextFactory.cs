using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using F1StatsAPI.Data;

namespace F1StatsAPI.Data
{
    public class F1StatsContextFactory : IDesignTimeDbContextFactory<F1StatsContext>
    {
        public F1StatsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<F1StatsContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=F1StatsDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new F1StatsContext(optionsBuilder.Options);
        }
    }
}
