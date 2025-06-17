using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrandPrixController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public GrandPrixController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrandPrix>>> GetGrandPrix()
        {
            return await _context.GrandPrix.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrandPrix>> GetGrandPrixById(int id)
        {
            var grandprix = await _context.GrandPrix.FindAsync(id);

            if (grandprix == null)
            {
                return NotFound();
            }

            return grandprix;
        }

        [HttpPost]
        public async Task<ActionResult<GrandPrix>> AddGrandPrix(GrandPrix grandPrix)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GrandPrix.Add(grandPrix);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while adding the Race.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            return CreatedAtAction(nameof(GetGrandPrixById), new { id = grandPrix.Id }, grandPrix);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrandPrix(GrandPrix grandprix, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grandprix.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var existingGrandPrix = await _context.GrandPrix.FindAsync(grandprix.Id);

            if (existingGrandPrix == null)
            {
                return NotFound();
            }

            existingGrandPrix.Name = grandprix.Name;
            existingGrandPrix.CircuitName = grandprix.CircuitName;
            existingGrandPrix.Date = grandprix.Date;
            existingGrandPrix.Laps = grandprix.Laps;
            existingGrandPrix.Distance = grandprix.Distance;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occured while updating Grand Prix.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrandPrix(int id)
        {
            var existingRace = await _context.GrandPrix.FindAsync(id);

            if (existingRace == null)
            {
                return NotFound();
            }

            _context.GrandPrix.Remove(existingRace);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while deleting Grand Prix.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
            
            return NoContent();
        }
    }
}
