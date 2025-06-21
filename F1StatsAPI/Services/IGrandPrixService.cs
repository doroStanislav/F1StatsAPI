using F1StatsAPI.Models;

namespace F1StatsAPI.Services
{
    public interface IGrandPrixService
    {
        Task<IEnumerable<GrandPrix>> GetAllGrandPrixAsync();
        Task<GrandPrix?> GetGrandPrixByIdAsync(int id);
        Task<GrandPrix?> AddGrandPrixAsync(GrandPrix grandPrix);
        Task<bool> UpdateGrandPrixAsync(int id, GrandPrix grandPrix);
        Task<bool> DeleteGrandPrixAsync(int id);
    }
}
