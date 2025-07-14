using AutoMapper;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;
using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;

namespace F1StatsAPI.Services
{
    public class DriverService : IDriverService
    {
        private readonly F1StatsContext _context;
        private readonly IMapper _mapper;

        public DriverService(F1StatsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriverDTO>> GetDriversAsync()
        {
            var drivers = await _context.Drivers
                .Where(d => d.IsActive)
                .Include(d => d.Team)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DriverDTO>>(drivers);
        }

        public async Task<DriverDTO?> GetDriverByIdAsync(int id)
        {
            var driver = await _context.Drivers
                .Include(d => d.Team)
                .FirstOrDefaultAsync(d => d.Id == id);

            return driver == null ? null : _mapper.Map<DriverDTO>(driver); 
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
