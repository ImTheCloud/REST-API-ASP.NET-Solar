using PROJETDOTNETQ5.Data;
using PROJETDOTNETQ5.Repositories;
using PROJETDOTNETQ5.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PROJETDOTNETQ5.Services
{
    // Service class for handling operations related to CelestialObjects
    public class CelestialObjectService
    {
        private readonly CelestialObjectRepository _celestialObjectRepository;

        // Constructor with dependency injection of CelestialObjectRepository
        public CelestialObjectService(CelestialObjectRepository celestialObjectRepository)
        {
            _celestialObjectRepository = celestialObjectRepository;
        }

        // Get all CelestialObjects
        public async Task<List<CelestialObject>> GetCelestialObjects()
        {
            return await _celestialObjectRepository.GetCelestialObjects();
        }

        // Get a specific CelestialObject by ID
        public async Task<CelestialObject> GetCelestialObject(int id)
        {
            return await _celestialObjectRepository.GetCelestialObject(id);
        }

        // Create a new CelestialObject
        public async Task<CelestialObject> CreateCelestialObject(CelestialObject celestialObject)
        {
            return await _celestialObjectRepository.CreateCelestialObject(celestialObject);
        }

        // Update an existing CelestialObject by ID
        public async Task<bool> UpdateCelestialObject(int id, CelestialObject celestialObject)
        {
            celestialObject.Id = id; // Ensure the correct ID is set
            return await _celestialObjectRepository.UpdateCelestialObject(celestialObject);
        }

        // Delete a CelestialObject by ID
        public async Task<bool> DeleteCelestialObject(int id)
        {
            return await _celestialObjectRepository.DeleteCelestialObject(id);
        }
    }
}