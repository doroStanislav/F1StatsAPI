using F1StatsAPI.Data;
using F1StatsAPI.Migrations;
using F1StatsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data.Common;
using F1StatsAPI.DTOs;
using F1StatsAPI.Services.Interfaces;

namespace F1StatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDTO>>> GetResults()
        {
            var results = await _resultService.GetResultsAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDTO>> GetResultById(int id)
        {
            var existingResult = await _resultService.GetResultByIdAsync(id);
            if (existingResult == null) return NotFound();

            return Ok(existingResult);
        }

        [HttpPost]
        public async Task<ActionResult<Result?>> AddResult(Result result)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validationResult = await _resultService.ValidateForeignKeys(result);
            if (validationResult != null)
                return BadRequest(validationResult);

            var savedResult = await _resultService.AddResultAsync(result);

            if (savedResult == null)
            {
                return StatusCode(500, "A database error occurred while adding the Result.");
            }

            return CreatedAtAction(nameof(GetResultById), new { id = savedResult.Id }, savedResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, Result result)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != result.Id) return BadRequest("ID mismatch");

            var validation = await _resultService.ValidateForeignKeys(result);
            if (validation != null) return BadRequest(validation);

            var existingResult = await _resultService.UpdateResultAsync(id, result);
            if (existingResult == false)
            {
                return StatusCode(500, "A database error occurred while updating the result.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var deletedResult = await _resultService.DeleteResultAsync(id);

            if (deletedResult == false) 
            {
                return StatusCode(500, "A database error occurred while deleting the result.");
            }

            return NoContent();
        }
    }
}
