using F1StatsAPI.Models;
using F1StatsAPI.Data;
using F1StatsAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Identity.Client;
using F1StatsAPI.Services.Interfaces;

namespace F1StatsAPI.Services
{
    public class TeamService : ITeamService
    {
        private readonly F1StatsContext _context;
        private readonly IMapper _mapper;
        public TeamService(F1StatsContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDTO>> GetTeamsAsync()
        {
            var teams = await _context.Teams
                .Include(c => c.Car)
                .Include(d => d.Drivers)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TeamDTO>>(teams);
        }

        public async Task<TeamDTO?> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams
                .Include(c => c.Car)
                .Include(d => d.Drivers)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (team == null) return null;
            
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<IEnumerable<Result>> GetTeamResultAsync(int id)
        {
            try
            {
                var team = await _context.Teams
                .Include(t => t.Results)
                .ThenInclude(t => t.GrandPrix)
                .FirstOrDefaultAsync(t => t.Id == id);

                return team?.Results ?? new List<Result>();
            }
            catch (Exception)
            {
                return new List<Result>();
            }
        }

        public async Task<Team?> AddTeamAsync(Team team)
        {
            try
            {
                await _context.Teams.AddAsync(team);
                await _context.SaveChangesAsync();
                return team;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateTeamAsync(int id, Team team)
        {
            try
            {
                var existingTeam = await _context.Teams.FindAsync(id);
                if (existingTeam == null) return false;

                existingTeam.Name = team.Name;
                existingTeam.TeamChief = team.TeamChief;
                existingTeam.WorldChampionships = team.WorldChampionships;
                existingTeam.BaseLocation = team.BaseLocation;
                existingTeam.FoundationYear = team.FoundationYear;

                existingTeam.CarId = team.CarId;

                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception) 
            { 
                return false; 
            }
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            try
            {
                var existingTeam = await _context.Teams.FindAsync(id);
                if (existingTeam == null) return false;

                _context.Teams.Remove(existingTeam);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string?> ValidateForeignKeys(Team team)
        {
            if (!await _context.Cars.AnyAsync(c => c.Id == team.CarId))
                return $"Car with ID {team.CarId} does not exist.";

            var driverIds = team.Drivers.Select(d => d.Id).ToList();
            var allExist = await _context.Drivers
                .Where(d => driverIds.Contains(d.Id))
                .CountAsync();
            if (allExist != driverIds.Count)
                return "One or more drivers do not exist.";

            return null;
        }
    }
}
