using PROJETDOTNETQ5.Data;
using PROJETDOTNETQ5.Repositories;
using PROJETDOTNETQ5.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

// Service class for handling operations related to Aliens
namespace PROJETDOTNETQ5.Services
{
    public class AlienService
    {
        private readonly AlienRepository _alienRepository;

        // Constructor with dependency injection of AlienRepository
        public AlienService(AlienRepository alienRepository)
        {
            _alienRepository = alienRepository;
        }

        // Get all Aliens
        public async Task<List<Alien>> GetAliens()
        {
            return await _alienRepository.GetAliens();
        }

        // Get a specific Alien by ID
        public async Task<Alien> GetAlien(int id)
        {
            return await _alienRepository.GetAlien(id);
        }

        // Create a new Alien
        public async Task<Alien> CreateAlien(Alien alien)
        {
            return await _alienRepository.CreateAlien(alien);
        }

        // Update an existing Alien by ID
        public async Task<bool> UpdateAlien(int id, Alien alien)
        {
            alien.Id = id; // Ensure the correct ID is set
            return await _alienRepository.UpdateAlien(alien);
        }

        // Delete an Alien by ID
        public async Task<bool> DeleteAlien(int id)
        {
            return await _alienRepository.DeleteAlien(id);
        }
    }
}