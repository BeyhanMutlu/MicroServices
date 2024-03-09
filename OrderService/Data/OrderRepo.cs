using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateOrder(int productId, Order order)
        {
            if(order ==null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            order.ProductId = productId;
            _context.Orders.Add(order);
        }

        public void CreateProduct(Product prd)
        {
            if(prd == null)
            {
                throw new ArgumentNullException(nameof(prd));
            }
            _context.Products.Add(prd);
        }

        public bool ExternalProductExist(int externalProductId)
        {
            return _context.Products.Any(p=>p.ExternalID == externalProductId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Order GetOrder(int productId, int orderId)
        {
            return _context.Orders.Where(c=>c.ProductId==productId &&c.Id==orderId).FirstOrDefault();
        }       
        public IEnumerable<Order> GetOrdersForProduct(int productId)
        {
            return _context.Orders.Where(c=>c.ProductId==productId).OrderBy(c=>c.Product.Model);
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p=>p.Id==productId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0);
        }
    }
}