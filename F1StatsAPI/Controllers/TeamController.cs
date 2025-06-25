using Microsoft.AspNetCore.Mvc;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Services;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            var teams = await _teamService.GetTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();

            return Ok(team);
        }

        [HttpGet("{id}/results")]
        public async Task<ActionResult<IEnumerable<Result>>> GetTeamResults(int id)
        {
            var teamResults = await _teamService.GetTeamResultAsync(id);
            if (!teamResults.Any()) return NotFound();

            return Ok(teamResults);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> AddTeam(Team team)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validationResult = await _teamService.ValidateForeignKeys(team);
            if (validationResult != null) return BadRequest(validationResult);
            
            var savedTeam = await _teamService.AddTeamAsync(team);

            if (savedTeam == null)
            {
                return StatusCode(500, "A database error occurred while saving the Team.");
            }

            return CreatedAtAction(nameof(GetTeam), new { id = savedTeam.Id }, savedTeam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, Team team)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != team.Id) return BadRequest("The ID in the URL does not match the ID in the request body.");

            var validationResult = await _teamService.ValidateForeignKeys(team);
            if (validationResult != null) return BadRequest(validationResult);

            var updatedTeam = await _teamService.UpdateTeamAsync(id, team);
            if (updatedTeam == false)
            {
                return StatusCode(500, "An error occurred while updating the Team.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var deletedTeam = await _teamService.DeleteTeamAsync(id);
            if (deletedTeam == false)
            {
                return StatusCode(500, "A database error occurred while deleting Team.");
            }

            return NoContent();
        }
    }
}
