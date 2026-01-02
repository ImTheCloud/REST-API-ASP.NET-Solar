using PROJETDOTNETQ5.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

// Service class for handling operations related to Satellites
namespace PROJETDOTNETQ5.Services
{
    public class SatelliteService
    {
        private readonly SatelliteRepository _satelliteRepository;

        // Constructor with dependency injection of SatelliteRepository
        public SatelliteService(SatelliteRepository satelliteRepository)
        {
            _satelliteRepository = satelliteRepository;
        }

        // Get all Satellites
        public async Task<List<Satellite>> GetSatellites()
        {
            return await _satelliteRepository.GetSatellites();
        }

        // Get a specific Satellite by ID
        public async Task<Satellite> GetSatellite(int id)
        {
            return await _satelliteRepository.GetSatellite(id);
        }

        // Create a new Satellite
        public async Task<Satellite> CreateSatellite(Satellite satellite)
        {
            return await _satelliteRepository.CreateSatellite(satellite);
        }

        // Update an existing Satellite by ID
        public async Task<bool> UpdateSatellite(int id, Satellite satellite)
        {
            satellite.Id = id; // Ensure the correct ID is set
            return await _satelliteRepository.UpdateSatellite(id, satellite);
        }

        // Delete a Satellite by ID
        public async Task<bool> DeleteSatellite(int id)
        {
            return await _satelliteRepository.DeleteSatellite(id);
        }
    }
}