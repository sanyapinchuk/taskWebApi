using WebServer.Models;

namespace WebServer.Data
{
    public class FridgeProductRepository : RepositoryBase<WebServer.Models.Fridge_Product>, Interfaces.IFridgeProductRepository
    {
        public FridgeProductRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async void AddNewProductAsync(int idFridge, int idProduct, int? count)
        {
            //get result quantity for product
            int addCount;
            var defaultCount = ((Product) (
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
        /// <param name="deleteAll"> true if need delete all products in fridge</param>
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
                var fr_pr = fridge_products.First();
                if(deleteAll)
                    await Task.Run(() =>DeleteAsync(fr_pr));
                else
                {
                    fr_pr.Quantity--;
                }
                return true;
            }

        }
    }
}
