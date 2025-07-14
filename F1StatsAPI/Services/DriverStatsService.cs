using AutoMapper;
using F1StatsAPI.DTOs;
using F1StatsAPI.Data;
using F1StatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using F1StatsAPI.Models;
using F1StatsAPI.Migrations;

namespace F1StatsAPI.Services
{
    public class DriverStatsService : IDriverStatsService
    {
        private readonly F1StatsContext _context;

        public DriverStatsService(F1StatsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriverStandingDTO>> GetStandingDTOsAsync()
        {
            var driverStanding = await _context.Results
                .Include(r => r.Driver)
                .Include(r => r.Team)
                .Include(r => r.GrandPrix)
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
                .ToListAsync();

            for (int i = 0; i < driverStanding.Count; i++)
            {
                driverStanding[i].Position = i + 1;
            }

            return driverStanding;
        }
    }


}
