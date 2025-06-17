using Microsoft.AspNetCore.Mvc;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public TeamController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpGet("{id}/results")]
        public async Task<ActionResult<IEnumerable<Result>>> GetTeamResults(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Results)
                .ThenInclude(t => t.GrandPrix)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team.Results);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> AddTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    

            _context.Teams.Add(team);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while saving the Team.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != team.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var existingTeam = await _context.Teams.FindAsync(id);

            if (existingTeam == null)
            {
                return NotFound();
            }

            existingTeam.Name = team.Name;
            existingTeam.TeamChief = team.TeamChief;
            existingTeam.WorldChampionships = team.WorldChampionships;
            existingTeam.BaseLocation = team.BaseLocation;
            existingTeam.FoundationYear = team.FoundationYear;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred while updating the Team.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var existingTeam = await _context.Teams.FindAsync(id);

            if (existingTeam == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(existingTeam);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while deleting Team.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

            return NoContent();
        }
    }
}
