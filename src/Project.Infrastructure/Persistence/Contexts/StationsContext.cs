using Microsoft.EntityFrameworkCore;
using Project.ApplicationCore.Entities.Models;

namespace Project.Infrastructure.Persistence.Contexts
{
    public class StationsContext : DbContext
    {
        public StationsContext(DbContextOptions<StationsContext> options) : base(options) 
        {
        }

        public DbSet<BikeStation> BikeStations { get; set; } 
    }
}
