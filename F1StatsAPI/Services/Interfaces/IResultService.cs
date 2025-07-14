using F1StatsAPI.Models;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Services.Interfaces
{
    public interface IResultService
    {
        Task<IEnumerable<ResultDTO>> GetResultsAsync();
        Task<ResultDTO?> GetResultByIdAsync(int id);
        Task<Result?> AddResultAsync(Result result);
        Task<bool> UpdateResultAsync(int id, Result result);
        Task<bool> DeleteResultAsync(int id);
        Task<string?> ValidateForeignKeys(Result result);
    }
}
