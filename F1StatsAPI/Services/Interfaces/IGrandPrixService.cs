using F1StatsAPI.DTOs;
using F1StatsAPI.Models;

namespace F1StatsAPI.Services.Interfaces
{
    public interface IGrandPrixService
    {
        Task<IEnumerable<GrandPrixDTO>> GetAllGrandPrixAsync();
        Task<GrandPrixDTO?> GetGrandPrixByIdAsync(int id);
        Task<GrandPrix?> AddGrandPrixAsync(GrandPrix grandPrix);
        Task<bool> UpdateGrandPrixAsync(int id, GrandPrix grandPrix);
        Task<bool> DeleteGrandPrixAsync(int id);
    }
}
