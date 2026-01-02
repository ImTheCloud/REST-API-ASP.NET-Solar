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

    // Controller class for handling HTTP requests related to Aliens
    public class AlienController : ControllerBase
    {
        private readonly AlienService _alienService;

        // Constructor with dependency injection of AlienService
        public AlienController(AlienService alienService)
        {
            _alienService = alienService;
        }

        // Get all Aliens
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Alien>>> GetAliens()
        {
            return Ok(await _alienService.GetAliens());
        }

        // Get a specific Alien by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Alien>> GetAlien(int id)
        {
            var alien = await _alienService.GetAlien(id);

            if (alien == null)
            {
                return NotFound();
            }

            return alien;
        }

        // Create a new Alien
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Alien>> Create(Alien alien)
        {
            var createdAlien = await _alienService.CreateAlien(alien);
            return CreatedAtAction(nameof(GetAlien), new { id = createdAlien.Id }, createdAlien);
        }

        // Update an existing Alien by ID
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(int id, Alien alien)
        {
            var result = await _alienService.UpdateAlien(id, alien);

            if (!result)
                return BadRequest();

            return Ok();
        }

        // Delete an Alien by ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAlien(int id)
        {
            var result = await _alienService.DeleteAlien(id);

            if (!result)
            {
                return NotFound("Incorrect alien id");
            }

            return Ok();
        }
    }
}
