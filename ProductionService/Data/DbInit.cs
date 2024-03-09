using Microsoft.EntityFrameworkCore;
using ProductionService.Models;

namespace ProductionService.Data
{
    public static class DbInit
    {
        public static void InitDb(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }
        private static void SeedData(AppDbContext context,bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("---> Apply migrations.");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"---> Could not run migrations: {ex.Message}");
                }                
            }

            if(!context.Products.Any())
            {
                Console.WriteLine("---> Seeding data.");   
                context.Products.AddRange(
                    new Product() {Brand="BMW",Model="i7",Cost="40000"},
                    new Product() {Brand="Mercedes",Model="c180",Cost="30000"},
                    new Product() {Brand="Audi",Model="a4",Cost="25000"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> We have already data in memory.");
            }
        }
    }
}