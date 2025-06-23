using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1StatsAPI.Data;
using F1StatsAPI.Models;
using F1StatsAPI.Services;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using F1StatsAPI.DTOs;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDTO>>> GetDrivers()
        {
            var drivers = await _driverService.GetDriversAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriverById(int id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();

            return Ok(driver);
        }

        [HttpPost]
        public async Task<ActionResult<Driver?>> AddDriver(Driver driver)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var savedDriver = await _driverService.AddDriverAsync(driver);

            if (savedDriver == null)
            {
                return StatusCode(500, "A database error occurred while adding the Driver.");
            }

            return CreatedAtAction(nameof(GetDriverById), new { id = savedDriver.Id }, savedDriver);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != driver.Id) return BadRequest("ID mismatch");

            var updatedDriver = await _driverService.UpdateDriverAsync(id, driver);

            if (updatedDriver == false)
            {
                return StatusCode(500, "A database error occured while updating the Driver.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            var deletedDriver = await _driverService.DeleteDriverAsync(id);

            if (deletedDriver == false)
            {
                return StatusCode(500, "A database error occured while deleting the Driver.");
            }

            return NoContent();
        }
    }   
}
