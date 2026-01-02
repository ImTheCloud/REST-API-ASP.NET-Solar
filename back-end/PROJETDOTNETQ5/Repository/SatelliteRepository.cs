using Microsoft.EntityFrameworkCore;
using PROJETDOTNETQ5.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using PROJETDOTNETQ5.Data;

// Repository class for handling database operations related to Satellites
namespace PROJETDOTNETQ5.Repositories
{
    public class SatelliteRepository
    {
        private readonly ApiDbContext _context;

        // Constructor with dependency injection of ApiDbContext
        public SatelliteRepository(ApiDbContext context)
        {
            _context = context;
        }

        // Get all Satellites
        public async Task<List<Satellite>> GetSatellites()
        {
            return await _context.Satellites.ToListAsync();
        }

        // Get a specific Satellite by ID
        public async Task<Satellite> GetSatellite(int id)
        {
            return await _context.Satellites.FindAsync(id);
        }

        // Create a new Satellite
        public async Task<Satellite> CreateSatellite(Satellite satellite)
        {
            _context.Add(satellite);
            await _context.SaveChangesAsync();
            return satellite;
        }

        // Update an existing Satellite
        public async Task<bool> UpdateSatellite(int id, Satellite satellite)
        {
            try
            {
                _context.Entry(satellite).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Log or handle the exception
                return false;
            }
        }

        // Delete a Satellite by ID
        public async Task<bool> DeleteSatellite(int id)
        {
            try
            {
                var satellite = await _context.Satellites.FindAsync(id);

                if (satellite == null)
                {
                    return false;
                }

                _context.Satellites.Remove(satellite);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                // Log or handle the exception
                return false;
            }
        }
    }
}