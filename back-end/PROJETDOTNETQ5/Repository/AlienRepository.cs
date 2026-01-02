using Microsoft.EntityFrameworkCore;
using PROJETDOTNETQ5.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

// Repository class for handling database operations related to Aliens
namespace PROJETDOTNETQ5.Repositories
{
    public class AlienRepository
    {
        private readonly ApiDbContext _context;

        // Constructor with dependency injection of ApiDbContext
        public AlienRepository(ApiDbContext context)
        {
            _context = context;
        }

        // Get all Aliens
        public async Task<List<Alien>> GetAliens()
        {
            return await _context.Aliens.ToListAsync();
        }

        // Get a specific Alien by ID
        public async Task<Alien> GetAlien(int id)
        {
            return await _context.Aliens.FindAsync(id);
        }

        // Create a new Alien
        public async Task<Alien> CreateAlien(Alien alien)
        {
            _context.Add(alien);
            await _context.SaveChangesAsync();
            return alien;
        }

        // Update an existing Alien
        public async Task<bool> UpdateAlien(Alien alien)
        {
            try
            {
                _context.Entry(alien).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Log or handle the exception
                return false;
            }
        }

        // Delete an Alien by ID
        public async Task<bool> DeleteAlien(int id)
        {
            try
            {
                var alien = await _context.Aliens.FindAsync(id);

                if (alien == null)
                {
                    return false;
                }

                _context.Aliens.Remove(alien);
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