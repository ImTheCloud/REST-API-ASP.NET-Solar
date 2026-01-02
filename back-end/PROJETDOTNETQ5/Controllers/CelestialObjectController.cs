// CelestialObjectController class
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

    // Controller class for handling HTTP requests related to CelestialObjects
    public class CelestialObjectController : ControllerBase
    {
        private readonly CelestialObjectService _celestialObjectService;

        // Constructor with dependency injection of CelestialObjectService
        public CelestialObjectController(CelestialObjectService celestialObjectService)
        {
            _celestialObjectService = celestialObjectService;
        }

        // Get all CelestialObjects
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CelestialObject>>> GetCelestialObject()
        {
            return Ok(await _celestialObjectService.GetCelestialObjects());
        }

        // Get a specific CelestialObject by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CelestialObject>> GetCelestialObject(int id)
        {
            var celestialObject = await _celestialObjectService.GetCelestialObject(id);

            if (celestialObject == null)
            {
                return NotFound();
            }

            return celestialObject;
        }

        // Create a new CelestialObject
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CelestialObject>> Create(CelestialObject celestialObject)
        {
            return Ok(await _celestialObjectService.CreateCelestialObject(celestialObject));
        }

        // Update an existing CelestialObject by ID
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(int id, CelestialObject celestialObject)
        {
            var result = await _celestialObjectService.UpdateCelestialObject(id, celestialObject);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // Delete a CelestialObject by ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCelestialObject(int id)
        {
            var result = await _celestialObjectService.DeleteCelestialObject(id);

            if (!result)
            {
                return NotFound("Incorrect celestialObject id");
            }

            return Ok();
        }

        // Get satellites for a specific CelestialObject by ID
        [HttpGet("{id}/satellites")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Satellite>>> GetSatellitesForCelestialObject(int id)
        {
            var celestialObject = await _celestialObjectService.GetCelestialObject(id);

            if (celestialObject == null)
            {
                return NotFound("Celestial object not found");
            }

            return Ok(celestialObject.Satellites.ToList());
        }

        // Get aliens for a specific CelestialObject by ID
        [HttpGet("{id}/aliens")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Alien>>> GetAliensForCelestialObject(int id)
        {
            var celestialObject = await _celestialObjectService.GetCelestialObject(id);

            if (celestialObject == null)
            {
                return NotFound("Celestial object not found");
            }

            return Ok(celestialObject.Aliens.ToList());
        }
    }
}