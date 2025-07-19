using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;
using F1StatsAPI.Repositories;

namespace F1StatsAPI.Services
{
    public class DriverStatsService : IDriverStatsService
    {
        private readonly IResultRepository _resultRepository;

        public DriverStatsService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<IEnumerable<DriverStandingDTO>> GetStandingDTOsAsync()
        {
            var results = await _resultRepository.GetAllAsync();
            var driverStanding = results
                .Where(r => r.Driver!.IsActive == true)
                .GroupBy(r => r.DriverId)
                .Select(group => new DriverStandingDTO
                {
                    DriverId = group.Key,
                    DriverName = group.First().Driver!.GivenName + " " + group.First().Driver!.FamilyName,
                    Nationality = group.First().Driver!.Country,
                    TeamName = group.OrderByDescending(r => r.GrandPrix!.Date).First().Team!.Name,
                    TotalPoints = group.Sum(d => d.Points ?? 0)
                })
                .OrderByDescending(dto => dto.TotalPoints)
                .ToList();

            for (int i = 0; i < driverStanding.Count; i++)
            {
                driverStanding[i].Position = i + 1;
            }

            return driverStanding;
        }
    }


}
