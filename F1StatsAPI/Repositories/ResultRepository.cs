using F1StatsAPI.Data;
using F1StatsAPI.Models;
using F1StatsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace F1StatsAPI.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly F1StatsContext _context;

        public ResultRepository (F1StatsContext context)
        {
            _context = context;
        }
        public async Task<List<Result>> GetAllAsync()
        {
            return await _context.Results
                .Include(r => r.Driver)
                .Include(r => r.Team)
                .Include(r => r.GrandPrix)
                .ToListAsync();
        }
    }
}
