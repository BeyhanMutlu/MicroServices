using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IProductionDataClient>();

                var products = grpcClient.ReturnAllProducts();

                SeedData(serviceScope.ServiceProvider.GetService<IOrderRepo>(),products);
            }
        }
        private static void SeedData(IOrderRepo repo, IEnumerable<Product> products)
        {            
            if(products.Count()> 0){
                Console.WriteLine($"---> Seeding new products.");
                foreach (var prd in products)
                {
                    if(!repo.ExternalProductExist(prd.ExternalID))
                    {
                        repo.CreateProduct(prd);
                    }
                    repo.SaveChanges();                
                }
            }

            
        }

    }
}