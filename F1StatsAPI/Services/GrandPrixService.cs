using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace F1StatsAPI.Services
{
    public class GrandPrixService : IGrandPrixService
    {
        private readonly F1StatsContext _context;

        public GrandPrixService(F1StatsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GrandPrix>> GetAllGrandPrixAsync()
        {
            return await _context.GrandPrix.ToListAsync();
        }

        public async Task<GrandPrix?> GetGrandPrixByIdAsync(int id)
        {
            return await _context.GrandPrix.FindAsync(id); 
        }

        public async Task<GrandPrix?> AddGrandPrixAsync(GrandPrix grandPrix)
        {
            try
            {
                await _context.GrandPrix.AddAsync(grandPrix);
                await _context.SaveChangesAsync();
                return grandPrix;
            }
            catch (Exception) 
            { 
                return null; 
            }
        }

        public async Task<bool> UpdateGrandPrixAsync(int id, GrandPrix grandPrix)
        {
            try
            {
                var existingGrandPrix = await _context.GrandPrix.FindAsync(id);
                if (existingGrandPrix == null) return false;

                existingGrandPrix.CircuitName = grandPrix.CircuitName;
                existingGrandPrix.Name = grandPrix.Name;
                existingGrandPrix.Date = grandPrix.Date;
                existingGrandPrix.Laps = grandPrix.Laps;
                existingGrandPrix.Distance = grandPrix.Distance;

                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception) 
            { 
                return false; 
            }
        }

        public async Task<bool> DeleteGrandPrixAsync(int id)
        {
            try
            {
                var existingGrandPrix = await _context.GrandPrix.FindAsync(id);
                if (existingGrandPrix == null) return false;

                _context.GrandPrix.Remove(existingGrandPrix);
                await _context.SaveChangesAsync();

                return true;
            }

            catch (Exception) 
            {
                return false; 
            }
        }
    }
}
