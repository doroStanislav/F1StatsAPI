using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamStatsController : ControllerBase
    {
        private readonly ITeamStatsService _teamStatsService;
        public TeamStatsController (ITeamStatsService teamStatsService)
        {
            _teamStatsService = teamStatsService;
        }

        [HttpGet("standings")]
        public async Task<ActionResult<IEnumerable<TeamStandingDTO>>> GetTeamStandingDTOs()
        {
            var standings = await _teamStatsService.GetTeamStandingDTOsAsync();
            return Ok(standings);
        }
    }
}
