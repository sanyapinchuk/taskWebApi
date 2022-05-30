using WebServer.Models;

namespace WebServer.Data.Interfaces
{
    public interface IFridgeProductRepository
    {
        public void AddNewProductAsync(int idFridge, int idProduct, int? count);
        public bool DeleteProductAsync(int idFridge, int idProduct, bool deleteAll = false);
        public IEnumerable<Fridge_Product> GetFridgeProductWithZeroQuantity();
        public Task<Fridge_Product?> GetFridgeProductAsync(int idFridge, int idProduct);
    }
}
