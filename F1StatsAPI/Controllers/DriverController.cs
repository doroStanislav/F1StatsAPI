using Microsoft.AspNetCore.Mvc;
using F1StatsAPI.Data;
using F1StatsAPI.Models;

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

        [HttpPost]
        public ActionResult<Driver> AddDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            _context.SaveChanges();

            var savedDriver = _context.Drivers.FirstOrDefault(d => 
                d.Code == driver.Code && 
                d.GivenName == driver.GivenName);

            return CreatedAtAction(nameof(GetDrivers), new {id = driver.Id}, driver);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(int id, Driver driver) 
        {
            var existingDriver = _context.Drivers.Find(id);
            if (existingDriver == null)
            {
                return NotFound();
            }
            
            existingDriver.Code = driver.Code;
            existingDriver.GivenName = driver.GivenName;
            existingDriver.FamilyName = driver.FamilyName;
            existingDriver.Country = driver.Country;
            existingDriver.DateOfBirth = driver.DateOfBirth;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteDriver(int id)
        {
            var driver = _context.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            _context.SaveChanges();
            return NoContent();
        }
    }   
}
