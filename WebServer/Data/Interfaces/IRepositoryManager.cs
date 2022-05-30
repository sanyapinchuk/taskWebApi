namespace WebServer.Data.Interfaces
{
    public interface IRepositoryManager
    {
        FridgeRepository Fridge { get; }
        FridgeModelRepository FridgeModel { get; }
        ProductRepository Product { get; } 
        FridgeProductRepository FridgeProduct { get; }

        public Task<bool> IsValidId(int idFridge, int idProduct);
        void Save();
    }
}
