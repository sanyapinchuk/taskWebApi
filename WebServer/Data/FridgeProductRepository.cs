using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeProductRepository : RepositoryBase<WebServer.Models.Fridge_Product>, Interfaces.IFridgeProductRepository
    {
        public FridgeProductRepository(DataContext dataContext) : base(dataContext)
        {

        }


        public IEnumerable<Fridge_Product> GetFridgeProductWithZeroQuantity()
        {
            return dataContext.Fridges_Products.FromSqlRaw("EXEC GetFridgesWithZeroQuantity");
        }

       

        public async Task<Fridge_Product?> GetFridgeProductAsync(int idFridge, int idProduct)
        {
            return await dataContext.Fridges_Products
                .Where(fr_pr => fr_pr.ProductId == idProduct)
                .Where(fr_pr => fr_pr.FridgeId == idFridge).FirstOrDefaultAsync();
        }

        /// <summary>
        /// add product to fridge (to table fridgeProduct)
        /// </summary>
        /// <param name="idFridge">Fridge id</param>
        /// <param name="idProduct"> Product id</param>
        /// <param name="count">count ot add. If null add defaultQuantity from table Product. If defaultQuantity == null, add 1</param>
        public void AddNewProductAsync(int idFridge, int idProduct, int? count)
        {
            //get result quantity for product
            int addCount;
            var defaultCount =((Product) (
                                        from product in dataContext.Products
                                        where product.Id == idProduct
                                        select product
                                ).First()).Default_quantity;
            if (count!=null)
            {
                addCount = (int)count;
            }
            else
            {
                if (defaultCount != null)
                    addCount = (int)defaultCount;
                else
                    addCount = 1;
            }
            
            var fridge_products = (
                    from pr_fr in dataContext.Fridges_Products
                    where pr_fr.FridgeId == idFridge
                    where pr_fr.ProductId == idProduct
                    select pr_fr
                    ).ToList();
            if (fridge_products.Count==0)
            {
                //add new product in fridge
                var fr_pr = new Fridge_Product();
                fr_pr.FridgeId = idFridge;
                fr_pr.ProductId = idProduct;
                fr_pr.Quantity = addCount;
                //dataContext.Fridges_Products.Add(fr_pr);
                //CreateAsync(fr_pr);
                dataContext.Fridges_Products.Add(fr_pr);
            }
            else
            {
                //increment quantity
                fridge_products.First().Quantity += addCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFridge"></param>
        /// <param name="idProduct"></param>
        /// <param name="deleteAll"> true if need delete all this type products in fridge</param>
        /// <returns>
        /// true if removed smth, false if not
        /// </returns>
        public async Task<bool> DeleteProductAsync(int idFridge, int idProduct, bool deleteAll = false)
        {

            var fridge_products = (
                    from pr_fr in dataContext.Fridges_Products
                    where pr_fr.FridgeId == idFridge
                    where pr_fr.ProductId == idProduct
                    select pr_fr
                    ).ToList();
            if (fridge_products.Count == 0)
            {
                // nothing to remove
                return false;
            }
            else
            {

                //increment quantity
                var fr_pr = fridge_products.FirstOrDefault();
                if(deleteAll)
                    Delete(fr_pr);
                else
                {
                    fr_pr.Quantity--;
                }
                return true;
            }

        }

    }
}
