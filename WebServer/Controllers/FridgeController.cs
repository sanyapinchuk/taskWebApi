using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;
using WebServer.Data.Interfaces;
using WebServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/Fridge")]    
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        /*public FridgeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }*/

         public FridgeController(IRepositoryManager repository)
         {
             this.repository = repository;
         }

        // GET: api/<FridgeController>
        // [Route("getAll")]
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<Fridge>>> GetAllFridges()
        {
            return Ok(await repository.Fridge.GetAllAsync());
        }


        [HttpGet]
        [Route("getFridge/{id}")]
        public async Task<ActionResult<Fridge>> GetFridge(int id)
        {
            return Ok(await repository.Fridge.GetFridgeByIdAsync(id));
        }

        // GET api/<FridgeController>/5
        [HttpGet]
        [Route("getProducts/{idFridge:int}")]
        public async Task<ActionResult<List<Product>>> Get(int idFridge)
        {           
            var fridge = await repository.Fridge.GetFridgeByIdAsync(idFridge);
            if(fridge!= null)
            {
                var a = await repository.Fridge.GetAllProductInFridge(idFridge);

                return Ok(a);
            }
            else
                return StatusCode(400, "Fridge doesn't exist");
        }


        [HttpPut]
        [Route("changeCountProducts")]
        public async Task<IActionResult> ChangeCountProductsInFridge([FromBody] Fridge_Product fridge_product)
        {
            if (fridge_product == null) 
                return StatusCode(400, "Shouldn't be null");
            else
            if(await repository.IsValidId(fridge_product.FridgeId,fridge_product.ProductId))
            {
                if (fridge_product.Quantity < 0)
                {
                    return StatusCode(400, "Count should be > 0");
                }
                var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync(fridge_product.FridgeId, fridge_product.ProductId);
                if(fr_pr!= null)
                {
                    fr_pr.Quantity = fridge_product.Quantity;

                    repository.Save();
                    return StatusCode(200, "Changed");
                }
                return StatusCode(400, "In this Fridge this product doesn't exist");
            }
            return StatusCode(400, "bad id");

        }


        // POST api/<FridgeController>
        /*[HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProductToFridge( int? idFridge,
                                        int? idProduct, [Optional] int? count)
        {
            if (idFridge == null || idProduct == null)
                return StatusCode(400, "Shouldn't be null");

            var fridge = await repository.Fridge.GetFridgeByIdAsync((int)idFridge);

            if(fridge == null)
            {
                return StatusCode(400, "Fridge doesn't exist");
            }    

            var product = await repository.Product.GetProductByIdAsync((int)idProduct);
            if(product == null)
            {
                return StatusCode(400, "Product doesn't exist");
            }
            if(count!= null&& count < 0)
            {
                return StatusCode(400, "Count should be > 0");
            }

            repository.FridgeProduct.AddNewProductAsync((int)idFridge, (int)idProduct, count);
            repository.Save();

            return StatusCode(200);

        }*/
        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProductToFridge([FromBody]Fridge_Product fr_pr)
        {
            

            var fridge = await repository.Fridge.GetFridgeByIdAsync(fr_pr.FridgeId);

            if (fridge == null)
            {
                return StatusCode(400, "Fridge doesn't exist");
            }

            var product = await repository.Product.GetProductByIdAsync((int)fr_pr.ProductId);
            if (product == null)
            {
                return StatusCode(400, "Product doesn't exist");
            }
            if (fr_pr.Quantity < 0)
            {
                return StatusCode(400, "Count should be > 0");
            }
            //if Quantity == 0 add default quantity
            repository.FridgeProduct.AddNewProductAsync(fr_pr.FridgeId, fr_pr.ProductId, 
                                                fr_pr.Quantity==0? null: fr_pr.Quantity);
            repository.Save();

            return StatusCode(201,"Added Product");

        }



        /// <summary>
        ///  create new fridge
        /// </summary>
        /// <param name="fridge"></param>
        /// <returns>id fridge</returns>
        [HttpPost]
        [Route("addFridge")]
        public async Task<ActionResult<int>> AddFridgeAsync([FromBody]Fridge fridge)
        {
            if (fridge.Name == null)
            {
                return StatusCode(400, "name shouldn't be null");
            }
            if (await repository.FridgeModel.GetFridgeModelAsync(fridge.FridgeModelId) != null)
            {
                //repository.Fridge.CreateFridge(fridge.Name, fridge.Owner_name, fridge.FridgeModelId);
                var addedFridge = await repository.Fridge.CreateFridge(fridge.Name, fridge.Owner_name, fridge.FridgeModelId);
                repository.Save();


                var result = StatusCode(201, addedFridge.Id);
                return result;
            }
            return StatusCode(400, "Bad modelId");
            
        }

        [HttpDelete]
        [Route("removeFridge/{id}")]
        public async Task<ActionResult<int>> RemoveFridgeAsync(int id)
        {
            var fridge = await repository.Fridge.GetFridgeByIdAsync(id);
            if (fridge != null)
            {
                repository.Fridge.Delete(fridge);
                repository.Save();
                return StatusCode(200, "Removed");
            }
            return StatusCode(204, "Fridge with this id doesn't exist");
        }


        /*
        [HttpPost] 
        [Route("addFridge")]
        public async Task<IActionResult> AddFridgeAsync(string Name, string Owner_name, int FridgeModelId)
        {
            if (Name == null)
            {
                return StatusCode(400, "name shouldn't be null");
            }
            if (repository.FridgeModel.GetFridgeModelAsync(FridgeModelId) != null)
            {
                await Task.Run(() => repository.Fridge.CreateFridge(Name, Owner_name, FridgeModelId));
                return StatusCode(201, "Created");
            }
            return StatusCode(400, "Bad modelId");

        }
        */

        [HttpPost]
        [Route("addDefaultProduct")]
        public async Task<IActionResult> AddProductDefaultQuantityPost()
        {
            var fr_prZeroProduct = repository.FridgeProduct.GetFridgeProductWithZeroQuantity();
            int countAdded = 0;
            foreach(var fridgeProduct in fr_prZeroProduct)
            {
                 /*  await Task.Run(() => repository.FridgeProduct
                               .AddNewProductAsync(fridgeProduct.FridgeId,
                               fridgeProduct.ProductId, null));*/
                repository.FridgeProduct
                               .AddNewProductAsync(fridgeProduct.FridgeId,
                               fridgeProduct.ProductId, null);
                countAdded++;
            }
            if(countAdded==0)
            {
                return StatusCode(200, "Nothing to add");
            }
            repository.Save();
            return StatusCode(200, $"Added {countAdded} products");
            
        }


        [HttpDelete]
        [Route("removeProduct/{idFridge}/{idProduct}")]      
        public async Task<IActionResult> RemoveProduct(int idFridge, int idProduct)
        {

            var fridge = await repository.Fridge.GetFridgeByIdAsync(idFridge);

            if (fridge == null)
            {
                return StatusCode(400, "Doesn't exist this idFridge");
            }

            var product = await repository.Product.GetProductByIdAsync(idProduct);
            if (product == null)
            {
                return StatusCode(400, "Doesn't exist this idProduct");
            }
            var resultDel = repository.FridgeProduct.DeleteProductAsync(idFridge, idProduct, true).Result;
            if(resultDel)
            {
                repository.Save();
                return StatusCode(200, "Deleted"); 
                
            }
            else
            {
                return StatusCode(404, "This fridge hasn't this product");
            }
        }



        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Fridge fridge)
        {
            var fridgeDb = await repository.Fridge.GetFridgeByIdAsync((int)id);
            if (fridgeDb != null)
            {
                fridge.Id = id;
                var fridgeModel = await repository.FridgeModel.GetFridgeModelAsync(fridge.FridgeModelId); 
                if(fridgeModel != null)
                {
                    repository.Fridge.UpdateAsync(fridge);
                    repository.Save();
                    return StatusCode(200, "Updated");
                }
                return StatusCode(400, "Fridge model doesn't exist");
                
            }
            else
            {
                return StatusCode(404, "Fridge doesn't exist");
            }

        }

    }
}
