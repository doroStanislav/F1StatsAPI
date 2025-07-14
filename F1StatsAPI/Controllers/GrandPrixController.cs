using F1StatsAPI.Data;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;


namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrandPrixController : ControllerBase
    {
        private readonly IGrandPrixService _grandPrixService;

        public GrandPrixController(IGrandPrixService grandPrixService)
        {
            _grandPrixService = grandPrixService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrandPrixDTO>>> GetGrandPrix()
        {
            var allGrandPrix = await _grandPrixService.GetAllGrandPrixAsync();
            return Ok(allGrandPrix);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrandPrixDTO?>> GetGrandPrixById(int id)
        {
            var grandprix = await _grandPrixService.GetGrandPrixByIdAsync(id);
            if (grandprix == null) return NotFound();

            return Ok(grandprix);
        }

        [HttpPost]
        public async Task<ActionResult<GrandPrix>> AddGrandPrix(GrandPrix grandPrix)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var savedGrandPrix = await _grandPrixService.AddGrandPrixAsync(grandPrix);

            if (savedGrandPrix == null) 
            {
                return StatusCode(500, "A database error occurred while adding the GrandPrix.");
            }

            return CreatedAtAction(nameof(GetGrandPrixById), new { id = savedGrandPrix.Id }, savedGrandPrix);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrandPrix(GrandPrix grandprix, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != grandprix.Id) return BadRequest("ID mismatch");

            var existingGrandPrix = await _grandPrixService.UpdateGrandPrixAsync(id, grandprix);

            if (existingGrandPrix == false)
            {
                return StatusCode(500, "A database error occured while updating Grand Prix.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrandPrix(int id)
        {
            var deletedGrandPrix = await _grandPrixService.DeleteGrandPrixAsync(id);

            if (deletedGrandPrix == false)
            {
                return StatusCode(500, "A database error occurred while deleting Grand Prix.");
            }

            return NoContent();
        }
    }
}
