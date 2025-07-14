using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDTO>> GetTeamsAsync();
        Task<TeamDTO?> GetTeamByIdAsync(int id);
        Task<IEnumerable<Result>> GetTeamResultAsync(int id);
        Task<Team?> AddTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(int id, Team team);
        Task<bool> DeleteTeamAsync(int id);
        Task<string?> ValidateForeignKeys(Team team);
    }
}
