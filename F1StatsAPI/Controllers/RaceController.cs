using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public RaceController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetRaces()
        {
            return await _context.Races.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _context.Races.FindAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return race;
        }

        [HttpPost]
        public async Task<ActionResult<Race>> AddRace(Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Races.Add(race);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbException)
            {
                return StatusCode(500, "A database error occurred while adding the Race.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpectex error occured.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRace(Race race, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != race.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var existingRace = await _context.Races.FindAsync(race.Id);

            if (existingRace == null)
            {
                return NotFound();
            }

            existingRace.Name = race.Name;
            existingRace.Country = race.Country;
            existingRace.Date = race.Date;
            existingRace.Laps = race.Laps;
            existingRace.Distance = race.Distance;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbException)
            {
                return StatusCode(500, "A database error occured while updating Race.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRace(int id)
        {
            var existingRace = await _context.Races.FindAsync(id);

            if (existingRace == null)
            {
                return NotFound();
            }

            _context.Races.Remove(existingRace);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbException)
            {
                return StatusCode(500, "A database error occurred while deleting Race.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
            
            return NoContent();
        }
    }
}
