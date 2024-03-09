using Microsoft.EntityFrameworkCore;
using ProductionService.Models;

namespace ProductionService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt)
        {
            
        }
        public DbSet<Product> Products {get; set;}
    }

}