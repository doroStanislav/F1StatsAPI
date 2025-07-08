using AutoMapper;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;
using F1StatsAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsAPI.Services
{
    public class GrandPrixService : IGrandPrixService
    {
        private readonly F1StatsContext _context;
        private readonly IMapper _mapper;

        public GrandPrixService(F1StatsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrandPrixDTO>> GetAllGrandPrixAsync()
        {
            var grandPrixList = await _context.GrandPrix.ToListAsync();
            return _mapper.Map<IEnumerable<GrandPrixDTO>>(grandPrixList);
        }

        public async Task<GrandPrixDTO?> GetGrandPrixByIdAsync(int id)
        {
            var grandPrix = await _context.GrandPrix.FindAsync(id);
            if (grandPrix == null) return null;

            return _mapper.Map<GrandPrixDTO>(grandPrix);
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
