using Microsoft.EntityFrameworkCore;
using PROJETDOTNETQ5.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Repository class for handling database operations related to CelestialObjects
namespace PROJETDOTNETQ5.Repositories
{
    public class CelestialObjectRepository
    {
        private readonly ApiDbContext _context;

        // Constructor with dependency injection of ApiDbContext
        public CelestialObjectRepository(ApiDbContext context)
        {
            _context = context;
        }

        // Get all CelestialObjects with related Aliens and Satellites
        public async Task<List<CelestialObject>> GetCelestialObjects()
        {
            return await _context.CelestialObjects
                .Include(c => c.Aliens)
                .Include(c => c.Satellites)
                .ToListAsync();
        }

        // Get a specific CelestialObject by ID with related Aliens and Satellites
        public async Task<CelestialObject> GetCelestialObject(int id)
        {
            var celestialObject = await _context.CelestialObjects
                .Include(c => c.Aliens)
                .Include(c => c.Satellites)
                .FirstOrDefaultAsync(co => co.Id == id);

            if (celestialObject != null)
            {
                Console.WriteLine($"CelestialObject {id} retrieved with {celestialObject.Aliens.Count} aliens and {celestialObject.Satellites.Count} satellites.");
            }
            else
            {
                Console.WriteLine($"CelestialObject {id} not found.");
            }

            return celestialObject;
        }

        // Create a new CelestialObject
        public async Task<CelestialObject> CreateCelestialObject(CelestialObject celestialObject)
        {
            _context.Add(celestialObject);
            await _context.SaveChangesAsync();
            return celestialObject;
        }

        // Update an existing CelestialObject
        public async Task<bool> UpdateCelestialObject(CelestialObject celestialObject)
        {
            try
            {
                _context.Entry(celestialObject).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Log or handle the exception
                return false;
            }
        }

        // Delete a CelestialObject by ID
        public async Task<bool> DeleteCelestialObject(int id)
        {
            try
            {
                var celestialObject = await _context.CelestialObjects.FindAsync(id);

                if (celestialObject == null)
                {
                    return false;
                }

                _context.CelestialObjects.Remove(celestialObject);
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
