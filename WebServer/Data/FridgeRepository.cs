using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeRepository : RepositoryBase<WebServer.Models.Fridge>, Interfaces.IFridgeRepository
    {
        public FridgeRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task<IList<(string Name, int Quantity)>> GetAllProductInFridge(int idFridge)
        {
            var objs =await ( from product in dataContext.Products
                       join pr_fr in dataContext.Fridges_Products
                       on product.Id equals pr_fr.ProductId
                       where pr_fr.FridgeId == idFridge
                       select new { product.Name, pr_fr.Quantity }
                       )
                       .ToListAsync();
            var list = new List<(string Name, int Quantity)>();
            foreach (var product in objs)
            {
                list.Add((product.Name, product.Quantity));
            }

            return list;
        }

        public async Task<Fridge?> GetFridgeByIdAsync(int idFridge)
        {
            return await dataContext.Fridges.Where(f => f.Id == idFridge)
                                                  .FirstOrDefaultAsync();
        }
    }
}
