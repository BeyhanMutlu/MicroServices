using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IOrderRepo
    {
        bool SaveChanges();
        
        #region product
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product prd);
        bool ProductExists(int productId);
        bool ExternalProductExist(int externalProductId);
        #endregion
        #region Order
        IEnumerable<Order>GetOrdersForProduct(int productId);
        Order GetOrder(int productId,int orderId);
        void CreateOrder(int productId,Order order);
        #endregion

    }
}