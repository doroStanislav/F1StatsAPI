using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using System.Threading.Tasks;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly F1StatsContext _context;

        public DriverController(F1StatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDrivers()
        {
            return _context.Drivers.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> AddDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Drivers.Add(driver);
            try 
            {
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while saving the driver.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            var savedDriver = _context.Drivers.FirstOrDefault(d => 
                d.Code == driver.Code && 
                d.GivenName == driver.GivenName);

            return CreatedAtAction(nameof(GetDriver), new {id = driver.Id}, driver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, Driver driver) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id !=  driver.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            var existingDriver = await _context.Drivers.FindAsync(id);

            if (existingDriver == null)
            {
                return NotFound();
            }
            
            existingDriver.Code = driver.Code;
            existingDriver.GivenName = driver.GivenName;
            existingDriver.FamilyName = driver.FamilyName;
            existingDriver.Country = driver.Country;
            existingDriver.DateOfBirth = driver.DateOfBirth;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while updating the driver.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);

            try
            {
               await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while deleting the driver.");
            }

            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }
            
            return NoContent();
        }
    }   
}
