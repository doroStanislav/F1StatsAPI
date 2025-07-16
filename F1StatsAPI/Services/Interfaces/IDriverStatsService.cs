using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Services.Interfaces
{
    public interface IDriverStatsService
    {
        Task<IEnumerable<DriverStandingDTO>> GetStandingDTOsAsync();
    }
}
