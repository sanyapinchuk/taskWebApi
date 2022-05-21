using WebServer.Models;

namespace WebServer.Data
{
    public class ProductRepository : RepositoryBase<WebServer.Models.Product>, Interfaces.IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task<Product?> GetProductByIdAsync(int idProduct)
        {
            return await dataContext.Products.Where(p => p.Id == idProduct).FirstOrDefaultAsync();
        }
    }
}
