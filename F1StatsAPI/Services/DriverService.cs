using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace F1StatsAPI.Services
{
    public class DriverService : IDriverService
    {
        private readonly F1StatsContext _context;

        public DriverService(F1StatsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetDriversAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver?> GetDriverByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task<Driver?> AddDriverAsync(Driver driver)
        {
            try
            {
                await _context.Drivers.AddAsync(driver);
                await _context.SaveChangesAsync();
                return driver;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateDriverAsync(int id, Driver driver)
        {
            try
            {
                var existingDriver = await _context.Drivers.FindAsync(id);
                if (existingDriver == null) return false;

                existingDriver.Code = driver.Code;
                existingDriver.GivenName = driver.GivenName;
                existingDriver.FamilyName = driver.FamilyName;
                existingDriver.Country = driver.Country;
                existingDriver.DateOfBirth = driver.DateOfBirth;

                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            try
            {
                var existingDriver = await _context.Drivers.FindAsync(id);
                if (existingDriver == null) return false;

                _context.Drivers.Remove(existingDriver);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                return false;
            } 
        }
    }
}
