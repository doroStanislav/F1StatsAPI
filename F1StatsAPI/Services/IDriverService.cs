using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDTO>> GetDriversAsync();
        Task<DriverDTO?> GetDriverByIdAsync(int id);
        Task<Driver?> AddDriverAsync(Driver driver);
        Task<bool> UpdateDriverAsync(int id, Driver driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
