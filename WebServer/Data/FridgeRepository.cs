using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeRepository : RepositoryBase<WebServer.Models.Fridge>, Interfaces.IFridgeRepository
    {
        public FridgeRepository(DataContext dataContext) : base(dataContext)
        {

        }

        /// <summary>
        ///  create in database new fridge
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Owner_name"></param>
        /// <param name="modelId"></param>
        /// <returns> id of created fridge</returns>
        public async Task<Fridge> CreateFridge(string Name, string? Owner_name, int modelId)
        {
            var fridge = new Fridge()
            {
                Name = Name,
                Owner_name = Owner_name,
                FridgeModelId = modelId
            };
            //fridge.Id = dataContext.Fridges.Count() + 10;
            dataContext.Fridges.Add(fridge);
            dataContext.SaveChanges();
            return await dataContext.Fridges.OrderBy<Fridge, int>(fridge=>fridge.Id).LastAsync();
        }

        public async Task<IList<Product>> GetAllProductInFridge(int idFridge)
        {
            var objs =await ( from product in dataContext.Products
                       join pr_fr in dataContext.Fridges_Products
                       on product.Id equals pr_fr.ProductId
                       where pr_fr.FridgeId == idFridge
                       select new { product, pr_fr.Quantity }
                       )
                       .ToListAsync();
            var list = new List<Product>();
            foreach (var productAnon in objs)
            {
                productAnon.product.Default_quantity = productAnon.Quantity;
                list.Add(productAnon.product);
            }

            return list;
        }

        public async Task<Fridge?> GetFridgeByIdAsync(int idFridge)
        {
            return await dataContext.Fridges.Where(f => f.Id == idFridge)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
        }
    }
}
