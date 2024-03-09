using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt)
        {
            
        }

        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Product>()
            .HasMany(p=> p.Orders)
            .WithOne(p=>p.Product!)
            .HasForeignKey(p=>p.ProductId);

            modelBuilder
            .Entity<Order>()
            .HasOne(p=> p.Product)
            .WithMany(p=>p.Orders)
            .HasForeignKey(p=>p.ProductId);


        }

    }

}