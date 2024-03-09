using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IProductionDataClient
    {
        IEnumerable<Product> ReturnAllProducts();
    }
}