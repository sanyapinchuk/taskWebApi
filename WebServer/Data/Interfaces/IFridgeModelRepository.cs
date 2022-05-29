using WebServer.Models;

namespace WebServer.Data.Interfaces
{
    public interface IFridgeModelRepository
    {
        public Task<FridgeModel?> GetFridgeModelAsync(int id);
    }
}
