using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;
using F1StatsAPI.Repositories;

namespace F1StatsAPI.Services
{
    public class TeamStatsService : ITeamStatsService
    {
        private readonly IResultRepository _resultRepository;

        public TeamStatsService (IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<IEnumerable<TeamStandingDTO>> GetTeamStandingsDTOsAsync()
        {
            var results = await _resultRepository.GetAllAsync();
            var teamStanding = results
                .GroupBy(t => t.TeamId)
                .Select(group => new TeamStandingDTO
                {
                    TeamId = group.Key,
                    TeamName = group.First().Team!.Name,
                    TotalPoints = group.Sum(t => t.Points ?? 0)
                })
                .OrderByDescending(dto => dto.TotalPoints)
                .ToList();
            for (int i = 0; i < teamStanding.Count; i++)
            {
                teamStanding[i].Position = i + 1;
            }

            return teamStanding;
        }
    }
}
