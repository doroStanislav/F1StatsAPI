using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Controllers;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;
using F1StatsAPI.DTOs;
using AutoMapper;

namespace F1StatsAPI.Services
{
    public class ResultService : IResultService
    {
        private readonly F1StatsContext _context;
        private readonly Mapper _mapper;
        public ResultService(F1StatsContext context, Mapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResultDTO>> GetResultsAsync()
        {
            var results = await _context.Results
                .Include(g => g.GrandPrix)
                .Include(t => t.Team)
                .Include(d => d.Driver)
                .Include(c => c.Car)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ResultDTO>>(results);
        }

        public async Task<ResultDTO?> GetResultByIdAsync(int id)
        {
            var result = await _context.Results
                .Include(g => g.GrandPrix)
                .Include(t => t.Team)
                .Include(d => d.Driver)
                .Include(c => c.Car)
                .FirstOrDefaultAsync(r => r.Id == id);
            return _mapper.Map<ResultDTO?>(result);
        }

        public async Task<Result?> AddResultAsync(Result result)
        {
            try
            {
                await _context.Results.AddAsync(result);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateResultAsync(int id, Result result)
        {
            try
            {
                var existingResult = await _context.Results.FindAsync(id);
                if (existingResult == null) return false;

                existingResult.GrandPrixId = result.GrandPrixId;
                existingResult.DriverId = result.DriverId;
                existingResult.TeamId = result.TeamId;
                existingResult.CarId = result.CarId;

                existingResult.Position = result.Position;
                existingResult.Points = result.Points;
                existingResult.GapToLeader = result.GapToLeader;
                existingResult.DidNotFinish = result.DidNotFinish;

                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception) 
            { 
                return false; 
            }
        }

        public async Task<bool> DeleteResultAsync(int id)
        {
            try
            {
                var existingResult = await _context.Results.FindAsync(id);
                if (existingResult == null) return false;

                _context.Results.Remove(existingResult);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string?> ValidateForeignKeys(Result result)
        {
            if (!await _context.GrandPrix.AnyAsync(g => g.Id == result.GrandPrixId))
                return $"GrandPrix with ID {result.GrandPrixId} does not exist.";

            if (!await _context.Teams.AnyAsync(t => t.Id == result.TeamId)) 
                return $"Team with ID {result.TeamId} does not exist.";

            if (!await _context.Drivers.AnyAsync(d => d.Id == result.DriverId))
                return $"Driver with ID {result.DriverId} does not exist";

            if (!await _context.Cars.AnyAsync(c => c.Id == result.CarId))
                return $"Car with ID {result.CarId} does not exist.";

            return null;
        }
    }
}
