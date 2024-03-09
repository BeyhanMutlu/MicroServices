using ProductionService.Models;

namespace ProductionService.Interfaces
{
    public interface IProductRepo
    {
        bool SaveChanges();

        Product GetProductById(int id);
        IEnumerable<Product> GetAllProducts();        
        void CreateProduct(Product prd);
        
    }
}