using WebServer.Models;

namespace WebServer.Data.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductByIdAsync(int idProduct);
    }
}
