using WebServer.Models;

namespace WebServer.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int idProduct);
    }
}
