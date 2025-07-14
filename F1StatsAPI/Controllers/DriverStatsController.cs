using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverStatsController : ControllerBase
    {
        private readonly IDriverStatsService _driverStatsService;
        public DriverStatsController(IDriverStatsService driverStatsService)
        {
            _driverStatsService = driverStatsService;
        }

        [HttpGet("standings")]
        public async Task<ActionResult<IEnumerable<DriverStandingDTO>>> GetDriverStanding()
        {
            var standings =  await _driverStatsService.GetStandingDTOsAsync();
            return Ok(standings);
        }
    }
}
