namespace WebServer.Data.Interfaces
{
    public interface IRepositoryManager
    {
        FridgeRepository Fridge { get; }
        FridgeModelRepository FridgeModel { get; }
        ProductRepository Product { get; } 
        FridgeProductRepository FridgeProduct { get; }
        void Save();
    }
}
