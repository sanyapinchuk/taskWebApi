namespace WebServer.Data
{
    public class FridgeModelRepository : RepositoryBase<WebServer.Models.FridgeModel>, Interfaces.IFridgeModelRepository
    {
        public FridgeModelRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
