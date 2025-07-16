using F1StatsAPI.Models;

namespace F1StatsAPI.Repositories
{
    public interface IResultRepository
    {
        Task<List<Result>> GetAllAsync();
    }
}
