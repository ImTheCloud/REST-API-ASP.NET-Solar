using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PROJETDOTNETQ5.Data;
using PROJETDOTNETQ5.Models;
using PROJETDOTNETQ5.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PROJETDOTNETQ5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Controller class for handling HTTP requests related to Satellites
    public class SatelliteController : ControllerBase
    {
        private readonly SatelliteService _satelliteService;

        // Constructor with dependency injection of SatelliteService
        public SatelliteController(SatelliteService satelliteService)
        {
            _satelliteService = satelliteService;
        }

        // Get all Satellites
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Satellite>>> GetSatellites()
        {
            return Ok(await _satelliteService.GetSatellites());
        }

        // Get a specific Satellite by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Satellite>> GetSatellite(int id)
        {
            var satellite = await _satelliteService.GetSatellite(id);

            if (satellite == null)
            {
                return NotFound();
            }

            return satellite;
        }

        // Create a new Satellite
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Satellite>> Create(Satellite satellite)
        {
            var createdSatellite = await _satelliteService.CreateSatellite(satellite);
            return CreatedAtAction(nameof(GetSatellite), new { id = createdSatellite.Id }, createdSatellite);
        }

        // Update an existing Satellite by ID
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(int id, Satellite satellite)
        {
            var result = await _satelliteService.UpdateSatellite(id, satellite);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // Delete a Satellite by ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteSatellite(int id)
        {
            var result = await _satelliteService.DeleteSatellite(id);

            if (!result)
            {
                return NotFound("Incorrect satellite id");
            }

            return Ok();
        }
    }
}