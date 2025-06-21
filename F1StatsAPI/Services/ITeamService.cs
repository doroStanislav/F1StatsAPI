using F1StatsAPI.Models;

namespace F1StatsAPI.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team?> GetTeamByIdAsync(int id);
        Task<IEnumerable<Result>> GetTeamResultAsync(int id);
        Task<Team?> AddTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(int id, Team team);
        Task<bool> DeleteTeamAsync(int id);
        Task<string?> ValidateForeignKeys(Team team);
    }
}
