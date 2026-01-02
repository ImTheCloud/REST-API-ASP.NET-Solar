using Microsoft.EntityFrameworkCore;
using System.Numerics;
using PROJETDOTNETQ5.Models;

// Database context class for handling interactions with the database
namespace PROJETDOTNETQ5.Data
{
    public class ApiDbContext : DbContext
    {
        // Constructor with dependency injection of DbContextOptions
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        // DbSet for CelestialObjects, representing the corresponding database table
        public DbSet<CelestialObject> CelestialObjects { get; set; }

        // DbSet for Aliens, representing the corresponding database table
        public DbSet<Alien> Aliens { get; set; }

        // DbSet for Satellites, representing the corresponding database table
        public DbSet<Satellite> Satellites { get; set; }
    }
}
