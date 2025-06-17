using F1StatsAPI.Data;
using F1StatsAPI.Migrations;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data.Common;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public ResultController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return await GetFullResultQuery().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var existingResult = await GetFullResultQuery().FirstOrDefaultAsync(r => r.Id == id);

            if (existingResult == null)
            {
                return NotFound();
            }

            return existingResult;
        }

        [HttpPost]
        public async Task<ActionResult> AddResult(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationResult = await ValidateForeignKeys(result);
            if (validationResult != null)
                return validationResult;

            await _context.Results.AddAsync(result);

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
        public async Task<IActionResult> UpdateResult(int id, Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingResult = await GetFullResultQuery().FirstOrDefaultAsync(r => r.Id == id);
            if (existingResult == null)
            {
                return NotFound();
            }

            var validation = await ValidateForeignKeys(result);
            if (validation != null)
            {
                return validation;
            }

            existingResult.GrandPrixId = result.GrandPrixId;
            existingResult.TeamId = result.TeamId;
            existingResult.DriverId = result.DriverId;
            existingResult.CarId = result.CarId;

            existingResult.Position = result.Position;
            existingResult.Time = result.Time;
            existingResult.Points = result.Points;
            existingResult.DidNotFinish = result.DidNotFinish;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while updating the result.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var existingResult = await _context.Results.FindAsync(id);

            if (existingResult == null)
            {
                return NotFound();
            }

            _context.Results.Remove(existingResult);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while deleting the result.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            return NoContent();
        }

        private IQueryable<Result> GetFullResultQuery()
        {
            return _context.Results
                .Include(g => g.GrandPrix)
                .Include(t => t.Team)
                .Include(d => d.Driver)
                .Include(c => c.Car);
        }

        private async Task<ActionResult?> ValidateForeignKeys(Result result)
        {
            if (!await _context.GrandPrix.AnyAsync(g => g.Id == result.GrandPrixId))
            {
                return BadRequest($"GrandPrix with ID {result.GrandPrixId} does not exist.");
            }

            if (!await _context.Teams.AnyAsync(t => t.Id == result.TeamId))
            {
                return BadRequest($"Team with ID {result.TeamId} does not exist.");
            }

            if (!await _context.Drivers.AnyAsync(d => d.Id == result.DriverId))
            {
                return BadRequest($"Driver with ID {result.DriverId} does not exist.");
            }

            if (!await _context.Cars.AnyAsync(c => c.Id == result.CarId))
            {
                return BadRequest($"Car with ID {result.CarId} does not exist.");
            }

            return null;
        }
    }
}
