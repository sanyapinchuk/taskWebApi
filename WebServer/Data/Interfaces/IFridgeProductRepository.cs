namespace WebServer.Data.Interfaces
{
    public interface IFridgeProductRepository
    {
        public void AddNewProductAsync(int idFridge, int idProduct, int? count);
        public Task<bool> DeleteProductAsync(int idFridge, int idProduct, bool deleteAll = false);
    }
}
