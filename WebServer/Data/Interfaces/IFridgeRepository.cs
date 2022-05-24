using WebServer.Models;

namespace WebServer.Data.Interfaces
{
    public interface IFridgeRepository
    {
        public Task<IList<Product>> GetAllProductInFridge(int idFridge);
        public Task<Fridge?> GetFridgeByIdAsync(int idFridge);
    }
}
