using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
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
        public async Task<IActionResult> ChangeCountProductsInFridge(int? idFridge, int? idProduct,int? count)
        {
            if (idFridge==null || idProduct == null || count == null)
                return StatusCode(400, "Shouldn't be null");
            if(await repository.IsValidId((int)idFridge,(int)idProduct))
            {
                if (count != null && count < 0)
                {
                    return StatusCode(400, "Count should be > 0");
                }
                var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync((int)idFridge, (int)idProduct);
                if(fr_pr!= null)
                {
                    fr_pr.Quantity = (int)count;
                    repository.Save();
                    return StatusCode(200, "Changed");
                }
                return StatusCode(400, "In this Fridge this product doesn't exist");
            }
            return StatusCode(400, "bad id");

        }


        // POST api/<FridgeController>
        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProductToFridge( int? idFridge, int? idProduct, [Optional] int? count)
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

        }


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



        // DELETE api/<FridgeController>/5
        [HttpDelete]
        [Route("removeProduct")]      
        public async Task<IActionResult> RemoveProduct(int? idFridge, int? idProduct, bool deleteAll=false)
        {
            if(idFridge == null || idProduct == null)
            {
                return StatusCode(400, "id shouldn't be null");
            }
            var fridge = await repository.Fridge.GetFridgeByIdAsync((int)idFridge);

            if (fridge == null)
            {
                return StatusCode(400, "Doesn't exist this idFridge");
            }

            var product = await repository.Product.GetProductByIdAsync((int)idProduct);
            if (product == null)
            {
                return StatusCode(400, "Doesn't exist this idProduct");
            }
            var resultDel = repository.FridgeProduct.DeleteProductAsync((int)idFridge, (int)idProduct, deleteAll).Result;
            if(resultDel)
            {
                return StatusCode(200, "Deleted"); 
                repository.Save();
            }
            else
            {
                return StatusCode(404, "This fridge hasn't this product");
            }
        }
    }
}
