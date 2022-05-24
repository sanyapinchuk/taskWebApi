﻿using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeRepository : RepositoryBase<WebServer.Models.Fridge>, Interfaces.IFridgeRepository
    {
        public FridgeRepository(DataContext dataContext) : base(dataContext)
        {

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
                                                  .FirstOrDefaultAsync();
        }
    }
}
