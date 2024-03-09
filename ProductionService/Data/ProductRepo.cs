using ProductionService.Interfaces;
using ProductionService.Models;

namespace ProductionService.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateProduct(Product prd)
        {
            if (prd == null)
            {
                throw new ArgumentNullException(nameof(prd));
            }

            _context.Products.Add(prd);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(i => i.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}