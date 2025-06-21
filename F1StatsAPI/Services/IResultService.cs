using F1StatsAPI.Models;

namespace F1StatsAPI.Services
{
    public interface IResultService
    {
        Task<IEnumerable<Result>> GetResultsAsync();
        Task<Result?> GetResultByIdAsync(int id);
        Task<Result?> AddResultAsync(Result result);
        Task<bool> UpdateResultAsync(int id, Result result);
        Task<bool> DeleteResultAsync(int id);
        Task<string?> ValidateForeignKeys(Result result);
    }
}
