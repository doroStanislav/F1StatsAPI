using F1StatsAPI.Models;

namespace F1StatsAPI.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver?> GetDriverByIdAsync(int id);
        Task<Driver?> AddDriverAsync(Driver driver);
        Task<bool> UpdateDriverAsync(int id, Driver driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
