namespace WebServer.Data.Interfaces
{
    public interface IFridgeProductRepository
    {
        public void AddNewProductAsync(int idFridge, int idProduct, int? count);
    }
}
