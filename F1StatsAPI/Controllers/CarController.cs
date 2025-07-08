using F1StatsAPI.Models;
using F1StatsAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public CarController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCars([FromBody] List<Car> cars)
        {
            if (cars == null || !cars.Any())
            {
                return BadRequest("Car list is empty.");
            }

            await _context.Cars.AddRangeAsync(cars);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{cars.Count} cars imported successfully." });
        }
    }
}
