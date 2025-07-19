using F1StatsAPI.DTOs;

namespace F1StatsAPI.Services.Interfaces
{
    public interface ITeamStatsService
    {
        Task<IEnumerable<TeamStandingDTO>> GetTeamStandingsDTOsAsync();
    }
}
