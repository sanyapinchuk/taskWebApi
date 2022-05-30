using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeModelRepository : RepositoryBase<WebServer.Models.FridgeModel>, Interfaces.IFridgeModelRepository
    {
        public FridgeModelRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task<FridgeModel?> GetFridgeModelAsync(int id)
        {
            var model = await dataContext.FridgeModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            return model;
        }
    }
}
